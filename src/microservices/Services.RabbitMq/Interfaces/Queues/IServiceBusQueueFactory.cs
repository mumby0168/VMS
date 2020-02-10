using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RabbitMq.Interfaces.Queues
{
    public interface IServiceBusQueueFactory
    {
        IServiceBusQueue CreateServiceBusQueue();
    }
}
