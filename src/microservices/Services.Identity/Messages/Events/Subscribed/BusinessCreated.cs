using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Events.Subscribed
{
    [MicroService(Common.Names.Services.Businesses)]
    public class BusinessCreated : IEvent
    {
        public Guid Id { get; }

        public int Code { get; }

        [JsonConstructor]
        public BusinessCreated(Guid id, int code)
        {
            Id = id;
            Code = code;
        }
    }
}
