using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    public interface IServiceBusMessageSubscriber
    {
        void Subscribe<T>(string queueName, Func<ServiceBusMessageBase<T>, Task> callback) where T : IServiceBusMessage;
    }
}
