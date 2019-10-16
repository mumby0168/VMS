using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    public interface IServiceBusMessageHandler
    {
        Task Handle<T>(object sender, BasicDeliverEventArgs args, Func<T, IRequestInfo, Task> callback) where T : IServiceBusMessage;
    }
}
