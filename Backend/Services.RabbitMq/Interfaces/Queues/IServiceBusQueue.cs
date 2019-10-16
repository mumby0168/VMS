using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RabbitMq.Interfaces.Queues
{
    public interface IServiceBusQueue
    {
        string Name { get; }
        void DeclareQueue(string name);

        void Bind(string exchangeName, string routingKey);
    }
}
