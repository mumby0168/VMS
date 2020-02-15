using System;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Business.Messages.Events
{
    public class BusinessCreated : IEvent
    {
        public BusinessCreated(Guid id, int code)
        {
            Id = id;
            Code = code;
        }

        public Guid Id { get; }

        public int Code { get; }
    }
}
