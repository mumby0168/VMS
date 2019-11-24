using System;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Sites
{
    [MicroService(ServiceNames.Sites)]
    public class SIteCreated : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public SIteCreated(Guid id)
        {
            Id = id;
        }
    }
}
