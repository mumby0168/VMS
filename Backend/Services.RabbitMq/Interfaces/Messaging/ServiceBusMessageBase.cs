using System;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public class ServiceBusMessageBase<T> where T : IServiceBusMessage
    {
        public Guid OperationId { get; set; }
        public T ServiceBusMessage { get; set; }
    }
}
    