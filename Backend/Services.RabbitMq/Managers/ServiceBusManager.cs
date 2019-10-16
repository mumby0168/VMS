using System;
using System.Reflection;
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
        private IServiceBusQueue _serviceBusQueue;
        private const string ExchangeName = "micro-service-exchange";

        public ServiceBusManager(IServiceBusConnection connection, IServiceBusExchangeFactory serviceBusExchangeFactory,
            IServiceBusQueueFactory serviceBusQueueFactory, IServiceBusMessageSubscriber serviceBusMessageSubscriber, IServiceSettings serviceSettings, IServiceBusConnectionFactory serviceBusConnectionFactory, IHandlerFactory handlerFactory)
        {
            _connection = connection;
            _serviceBusExchangeFactory = serviceBusExchangeFactory;
            _serviceBusQueueFactory = serviceBusQueueFactory;
            _serviceBusMessageSubscriber = serviceBusMessageSubscriber;
            _serviceSettings = serviceSettings;
            _serviceBusConnectionFactory = serviceBusConnectionFactory;
            _handlerFactory = handlerFactory;
        }

        public void CreateConnection(IServiceBusSettings serviceBusSettings, IServiceSettings serviceSettings)
        {
            var factory = _serviceBusConnectionFactory.CreateConnectionFactory(serviceBusSettings.HostName);
            var connection = factory.CreateConnection();

            _connection.RegisterConnection(connection);
            _connection.ServiceSetting = serviceSettings;

            var exchange = _serviceBusExchangeFactory.CreateServiceBusExchange();
            exchange.CreateExchange(ExchangeName, "topic");

            _serviceBusQueue =_serviceBusQueueFactory.CreateServiceBusQueue();
            _serviceBusQueue.DeclareQueue(serviceSettings.Name);
            _serviceSettings.Name = serviceSettings.Name;
        }

        public IServiceBusManager SubscribeCommand<T>(string serviceNamespace = null) where T : ICommand
        {
            var handler = _handlerFactory.ResolveCommandHandler<T>();
            var serviceFrom = GetServiceName<T>() ?? serviceNamespace;
            if (serviceFrom == null) throw new InvalidOperationException("Please specify the namespace the message is coming from through the [MicroService] attribute or by passing it through this function");
            _serviceBusMessageSubscriber.Subscribe<T>(_serviceSettings.Name, async (message, info) => await handler.HandleAsync(message, info));
            _serviceBusQueue.Bind(ExchangeName, $"{serviceFrom}.{typeof(T).Name}");
            return this;
        }

        public IServiceBusManager SubscribeEvent<T>(string serviceNamespace = null) where T : IEvent
        {
            var handler = _handlerFactory.ResolveEventHandler<T>();
            var serviceFrom = GetServiceName<T>() ?? serviceNamespace;
            if(serviceFrom == null) throw new InvalidOperationException("Please specify the namespace the message is coming from through the [MicroService] attribute or by passing it through this function");
            _serviceBusMessageSubscriber.Subscribe<T>(_serviceSettings.Name, async (message, info) => await handler.HandleAsync(message, info));
            _serviceBusQueue.Bind(ExchangeName, $"{serviceFrom}.{typeof(T).Name}");
            return this;
        }

        private string GetServiceName<T>() where T : IServiceBusMessage
            => typeof(T).GetCustomAttribute<MicroService>()?.ServiceName;

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}

