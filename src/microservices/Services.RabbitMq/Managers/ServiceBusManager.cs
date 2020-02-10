using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Exchange;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Interfaces.Queues;
using Services.RabbitMq.Interfaces.Settings;
using Services.RabbitMq.Messages;

namespace Services.RabbitMq.Managers
{
    public class ServiceBusManager : IServiceBusManager
    {
        private readonly IServiceBusConnection _connection;
        private readonly IServiceBusExchangeFactory _serviceBusExchangeFactory;
        private readonly IServiceBusQueueFactory _serviceBusQueueFactory;
        private readonly IServiceBusMessageSubscriber _serviceBusMessageSubscriber;
        private readonly IServiceSettings _serviceSettings;
        private readonly IServiceBusConnectionFactory _serviceBusConnectionFactory;
        private readonly IHandlerFactory _handlerFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ServiceBusManager> _logger;
        private const string ExchangeName = "micro-service-exchange";

        public ServiceBusManager(IServiceBusConnection connection, IServiceBusExchangeFactory serviceBusExchangeFactory,
            IServiceBusQueueFactory serviceBusQueueFactory, IServiceBusMessageSubscriber serviceBusMessageSubscriber, IServiceSettings serviceSettings, IServiceBusConnectionFactory serviceBusConnectionFactory, IHandlerFactory handlerFactory, IServiceProvider serviceProvider, ILogger<ServiceBusManager> logger)
        {
            _connection = connection;
            _serviceBusExchangeFactory = serviceBusExchangeFactory;
            _serviceBusQueueFactory = serviceBusQueueFactory;
            _serviceBusMessageSubscriber = serviceBusMessageSubscriber;
            _serviceSettings = serviceSettings;
            _serviceBusConnectionFactory = serviceBusConnectionFactory;
            _handlerFactory = handlerFactory;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void CreateConnection(IServiceBusSettings serviceBusSettings, IServiceSettings serviceSettings, bool declareExchange = true)
        {
            var factory = _serviceBusConnectionFactory.CreateConnectionFactory(serviceBusSettings.HostName);
            var connection = factory.CreateConnection();

            _connection.RegisterConnection(connection);
            _connection.ServiceSetting = serviceSettings;

            if (declareExchange)
            {
                var exchange = _serviceBusExchangeFactory.CreateServiceBusExchange();
                exchange.CreateExchange(serviceSettings.Name, "topic");
            }

            _logger.LogInformation("Connection made to rabbit MQ bus.");

            _serviceSettings.Name = serviceSettings.Name;
        }

        public IServiceBusManager SubscribeCommand<T>(string serviceNamespace = null) where T : ICommand
        {
            var handler = _handlerFactory.ResolveCommandHandler<T>();
            if (handler is null) throw new InvalidOperationException("Please register the command handler for: " + typeof(T).Name);
            var serviceFrom = _serviceSettings.Name;
            var queue = _serviceBusQueueFactory.CreateServiceBusQueue();
            var name = $"{serviceFrom}/{typeof(T).Name}";
            queue.DeclareQueue(name);
            queue.Bind(_serviceSettings.Name, $"{serviceFrom}.{typeof(T).Name}");
            _serviceBusMessageSubscriber.Subscribe<T>(name, async (message, info) => await handler.HandleAsync(message, info));
            return this;
        }

        public IServiceBusManager SubscribeEvent<T>(string serviceNamespace = null) where T : IEvent
        {
            var handler = _handlerFactory.ResolveEventHandler<T>();
            if (handler is null) throw new InvalidOperationException("Please register the event handler for: " + typeof(T).Name);
            var serviceFrom = GetServiceName<T>() ?? serviceNamespace;

            if(serviceFrom == null) throw new InvalidOperationException("Please specify the namespace the message is coming from through the [MicroService] attribute or by passing it through this function");

            string queueName = $"{_serviceSettings.Name}/{typeof(T).Name}";
            var queue = _serviceBusQueueFactory.CreateServiceBusQueue();
            queue.DeclareQueue(queueName);
            queue.Bind(serviceFrom, $"{serviceFrom}.{typeof(T).Name}");

            _serviceBusMessageSubscriber.Subscribe<T>(queueName, async (message, info) => await handler.HandleAsync(message, info));
            return this;
        }

        public IServiceBusManager SubscribeAllMessages<T>(Assembly currentAssembly, IEnumerable<Type> excludedTypes = null) where T : IGenericBusHandler
        {
            var types = currentAssembly.GetTypes();
            var eventTypes =
                types.Where(t => typeof(IEvent).IsAssignableFrom(t) || typeof(ICommand).IsAssignableFrom(t));

            if (excludedTypes != null)
            {
                eventTypes = eventTypes.Where(t => !excludedTypes.Contains(t));
            }


            var handler = (T)_serviceProvider.GetService(typeof(IGenericBusHandler));

            foreach (var eventType in eventTypes)
            {
                var serviceFrom = GetServiceName(eventType);
                if (serviceFrom == null) throw new InvalidOperationException("Please specify the namespace the message is coming from through the [MicroService] attribute or by passing it through this function");
                string queueName = $"{_serviceSettings.Name}/{typeof(T).Name}";
                var queue = _serviceBusQueueFactory.CreateServiceBusQueue();
                queue.DeclareQueue(queueName);
                queue.Bind(serviceFrom, $"{serviceFrom}.{eventType.Name}");
                _serviceBusMessageSubscriber.CustomSubscribe(queueName, async (message, info) => await handler.HandleAsync(message, info));
            }

            return this;
        }

        private string GetServiceName<T>() where T : IServiceBusMessage
            => typeof(T).GetCustomAttribute<MicroService>()?.ServiceName;


        private string GetServiceName(Type t)
            => t.GetCustomAttribute<MicroService>()?.ServiceName;

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}

