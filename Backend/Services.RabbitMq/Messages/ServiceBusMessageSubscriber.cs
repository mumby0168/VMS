using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    public class ServiceBusMessageSubscriber : IServiceBusMessageSubscriber
    {
        private readonly IServiceBusConnectionFactory _serviceBusConnectionFactory;
        private readonly IServiceBusMessageHandler _serviceBusMessageHandler;
        private readonly IServiceBusConsumerFactory _factory;
        private readonly ILogger<ServiceBusMessageSubscriber> _logger;

        public ServiceBusMessageSubscriber(IServiceBusConnectionFactory serviceBusConnectionFactory, IServiceBusMessageHandler serviceBusMessageHandler, IServiceBusConsumerFactory factory, ILogger<ServiceBusMessageSubscriber> logger)
        {
            _serviceBusConnectionFactory = serviceBusConnectionFactory;
            _serviceBusMessageHandler = serviceBusMessageHandler;
            _factory = factory;
            _logger = logger;
        } 

        public void Subscribe<T>(string queueName, Func<T,IRequestInfo, Task> callback) where T : IServiceBusMessage
        {
            var connection = _serviceBusConnectionFactory.ResolveServiceBusConnection();
            var consumer = _factory.CreateBasicConsumer(connection.Channel);
            consumer.Received += (sender, args) => _serviceBusMessageHandler.Handle(sender, args, callback);
            connection.Channel.BasicConsume(queueName, true, consumer);
        }
        public void CustomSubscribe(string queueName, Func<IServiceBusMessage, IRequestInfo, Task> callback)
        {
            var connection = _serviceBusConnectionFactory.ResolveServiceBusConnection();
            var consumer = _factory.CreateBasicConsumer(connection.Channel);
            consumer.Received += (sender, args) => _serviceBusMessageHandler.HandleUsingRoutingKey(sender, args, callback);
            connection.Channel.BasicConsume(queueName, true, consumer);
        }
    }
}
