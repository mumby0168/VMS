using System;
using System.Diagnostics.CodeAnalysis;
using RabbitMQ.Client;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Factories;

namespace Services.RabbitMq.Factories
{
    [ExcludeFromCodeCoverage]
    public class ServiceBusConnectionFactory : IServiceBusConnectionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceBusConnectionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IServiceBusConnection ResolveServiceBusConnection()  
        {
            return (IServiceBusConnection) _serviceProvider.GetService(typeof(IServiceBusConnection));
        }

        public ConnectionFactory CreateConnectionFactory(string hostName)
        {
            return new ConnectionFactory() {HostName = hostName};
        }
    }
}
