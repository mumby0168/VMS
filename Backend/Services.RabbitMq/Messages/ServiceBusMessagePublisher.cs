using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Framing;
using Services.Common.Logging;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Interfaces.Settings;
using Services.RabbitMq.Interfaces.Wrappers;

namespace Services.RabbitMq.Messages
{
    public class ServiceBusMessagePublisher : IServiceBusMessagePublisher
    {
        private readonly IServiceBusConnectionFactory _serviceBusConnectionFactory;
        private readonly IServiceSettings _serviceSettings;
        private readonly IJsonConvertWrapper _jsonConvertWrapper;
        private readonly IUtf8Wrapper _utf8Wrapper;
        private readonly IVmsLogger<ServiceBusMessagePublisher> _logger;

        public ServiceBusMessagePublisher(IServiceBusConnectionFactory serviceBusConnectionFactory, IServiceSettings serviceSettings, IJsonConvertWrapper jsonConvertWrapper, IUtf8Wrapper utf8Wrapper, IVmsLogger<ServiceBusMessagePublisher> logger)
        {
            _serviceBusConnectionFactory = serviceBusConnectionFactory;
            _serviceSettings = serviceSettings;
            _jsonConvertWrapper = jsonConvertWrapper;
            _utf8Wrapper = utf8Wrapper;
            this._logger = logger;
        }


        void Publish<T>(T message, IRequestInfo requestInfo, string exchange, string routingKey) where T : IServiceBusMessage
        {
            var messageJson = _jsonConvertWrapper.Serialize(message);
            var requestJson = _jsonConvertWrapper.Serialize(requestInfo);
            var json = requestJson + "\t" + messageJson;
            var body = _utf8Wrapper.GetBytes(json);
            var connection = _serviceBusConnectionFactory.ResolveServiceBusConnection();
            connection.Channel.BasicPublish(exchange, routingKey, true, new BasicProperties(), body);
            _logger.LogInformation($"Published message with key: {routingKey}", "RabbitMq");
        }


        public void PublishCommand<T>(T message, IRequestInfo requestInfo) where T : ICommand
        {
            string exchange = GetServiceName<T>();
            if(exchange == null) throw new InvalidOperationException("Please specify using the [MicroService] attribute which service the command is intended for.");
            string key = $"{exchange}.{typeof(T).Name}";
            Publish(message, requestInfo, exchange, key);
        }


        public void PublishEvent<T>(T message, IRequestInfo requestInfo) where T : IEvent
        {
            Publish(message, requestInfo, _serviceSettings.Name, $"{_serviceSettings.Name}.{typeof(T).Name}");
        }

        private string GetServiceName<T>() where T : IServiceBusMessage
            => typeof(T).GetCustomAttribute<MicroService>()?.ServiceName;

    }
}
