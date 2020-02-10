using System;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Business.Messages.Events
{
    public class BusinessCreated : IEvent
    {
        public BusinessCreated(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
