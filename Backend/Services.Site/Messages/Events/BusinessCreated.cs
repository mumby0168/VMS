using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Messages.Events
{
    [MicroService(ServiceNames.Businesses)]
    public class BusinessCreated : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public BusinessCreated(Guid id)
        {
            Id = id;
        }

        public BusinessCreated()
        {
            
        }
    }
}
