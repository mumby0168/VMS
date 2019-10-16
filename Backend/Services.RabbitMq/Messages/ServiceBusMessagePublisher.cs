using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Framing;
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

        public ServiceBusMessagePublisher(IServiceBusConnectionFactory serviceBusConnectionFactory, IServiceSettings serviceSettings, IJsonConvertWrapper jsonConvertWrapper, IUtf8Wrapper utf8Wrapper)
        {
            _serviceBusConnectionFactory = serviceBusConnectionFactory;
            _serviceSettings = serviceSettings;
            _jsonConvertWrapper = jsonConvertWrapper;
            _utf8Wrapper = utf8Wrapper;
        }


        void Publish<T>(T message, IRequestInfo requestInfo) where T : IServiceBusMessage
        { 
            string routingKey = $"{_serviceSettings.Name}.{typeof(T).Name}";
            var messageJson = _jsonConvertWrapper.Serialize(message);
            var requestJson = _jsonConvertWrapper.Serialize(requestInfo);
            var json = requestJson + "." + message;
            var body = _utf8Wrapper.GetBytes(json);
            var connection = _serviceBusConnectionFactory.ResolveServiceBusConnection();
            connection.Channel.BasicPublish("micro-service-exchange", routingKey, true, new BasicProperties(), body);
        }


        public void PublishCommand<T>(T message, IRequestInfo requestInfo) where T : ICommand =>
            Publish(message, requestInfo);

        public void PublishEvent<T>(T message, IRequestInfo requestInfo) where T : IEvent =>
            Publish(message, requestInfo);
    }
}
