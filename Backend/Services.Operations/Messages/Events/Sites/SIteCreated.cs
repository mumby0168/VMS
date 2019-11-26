using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Sites
{
    [MicroService(ServiceNames.Sites)]
    public class SiteCreated : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public SiteCreated(Guid id)
        {
            Id = id;
        }
    }
}
