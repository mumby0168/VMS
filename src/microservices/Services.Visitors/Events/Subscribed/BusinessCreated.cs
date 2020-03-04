using System;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Events.Subscribed
{
    [MicroService(Common.Names.Services.Businesses)]
    public class BusinessCreated : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor()]
        public BusinessCreated(Guid id)
        {
            Id = id;
        }
    }
}