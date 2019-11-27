using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Sites
{
    [MicroService(ServiceNames.Sites)]
    public class RemoveSiteResource : ICommand
    {
        public Guid ResourceId { get; }

        [JsonConstructor]
        public RemoveSiteResource(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
