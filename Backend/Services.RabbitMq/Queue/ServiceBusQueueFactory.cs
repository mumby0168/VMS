using System;
using Microsoft.Extensions.DependencyInjection;
using Services.RabbitMq.Interfaces.Queues;

namespace Services.RabbitMq.Queue
{
    class ServiceBusQueueFactory : IServiceBusQueueFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceBusQueueFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public IServiceBusQueue CreateServiceBusQueue()
        {
            return _serviceProvider.GetService<IServiceBusQueue>();
        }
    }
}
