using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Businesses
{
    [MicroService(Common.Names.Services.Businesses)]
    public class BusinessCreated : IEvent
    {
        [JsonConstructor]
        public BusinessCreated(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
