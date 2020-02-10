using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Exchange;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Queues;

namespace Services.RabbitMq.Queue
{
    public class ServiceBusQueue : IServiceBusQueue
    {
        private readonly IServiceBusConnectionFactory _serviceBusConnectionFactory;
        private readonly IServiceBusExchangeFactory _serviceBusExchangeFactory;

        public ServiceBusQueue(IServiceBusConnectionFactory serviceBusConnectionFactory, IServiceBusExchangeFactory serviceBusExchangeFactory)
        {
            _serviceBusConnectionFactory = serviceBusConnectionFactory;
            _serviceBusExchangeFactory = serviceBusExchangeFactory;
        }

        public string Name { get; private set; }

        public void DeclareQueue(string name)
        {
            var connection = _serviceBusConnectionFactory.ResolveServiceBusConnection();
            connection.Channel.QueueDeclare(name, false, false, true, null);
            Name = name;
        }

        public void Bind(string exchangeName, string routingKey)
        {
            var connection = _serviceBusConnectionFactory.ResolveServiceBusConnection();
            var exchange = _serviceBusExchangeFactory.CreateServiceBusExchange();
            exchange.CreateExchange(exchangeName, "topic");
            connection.Channel.QueueBind(Name, exchangeName, routingKey, null);
        }
    }
}
