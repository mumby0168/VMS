using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    public interface IServiceBusMessageSubscriber
    {
        void Subscribe<T>(string queueName, Func<T,IRequestInfo, Task> callback) where T : IServiceBusMessage;
        void CustomSubscribe(string queueName, Func<IServiceBusMessage, IRequestInfo, Task> callback);
    }
}
