using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Services.RabbitMq.Interfaces.Exchange;

namespace Services.RabbitMq.Exchange
{
    public class ServiceBusExchangeFactory : IServiceBusExchangeFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceBusExchangeFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IServiceBusExchange CreateServiceBusExchange()
        {
            return _serviceProvider.GetService<IServiceBusExchange>();
        }
    }
}
