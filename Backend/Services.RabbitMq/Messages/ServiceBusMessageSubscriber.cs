using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

        public ServiceBusMessageSubscriber(IServiceBusConnectionFactory serviceBusConnectionFactory, IServiceBusMessageHandler serviceBusMessageHandler, IServiceBusConsumerFactory factory)
        {
            _serviceBusConnectionFactory = serviceBusConnectionFactory;
            _serviceBusMessageHandler = serviceBusMessageHandler;
            _factory = factory;
        } 

        public void Subscribe<T>(string queueName, Func<T,IRequestInfo, Task> callback) where T : IServiceBusMessage
        {
            var connection = _serviceBusConnectionFactory.ResolveServiceBusConnection();
            var consumer = _factory.CreateBasicConsumer(connection.Channel);
            consumer.Received += (sender, args) => _serviceBusMessageHandler.Handle(sender, args, callback);
            connection.Channel.BasicConsume(queueName, true, consumer);
        }

    }
}
