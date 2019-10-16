using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Exchange;
using Services.RabbitMq.Interfaces.Factories;

namespace Services.RabbitMq.Exchange
{
    public class ServiceBusExchange : IServiceBusExchange
    {
        private readonly IServiceBusConnectionFactory _serviceBusConnectionFactory;

        public ServiceBusExchange(IServiceBusConnectionFactory serviceBusConnectionFactory)
        {
            _serviceBusConnectionFactory = serviceBusConnectionFactory;
        }
        public void CreateExchange(string name, string type)
        {
            var connection = _serviceBusConnectionFactory.ResolveServiceBusConnection();
            connection.Channel.ExchangeDeclare(name,type, false, false, null);
        }
    }
    }
