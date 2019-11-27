using System;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Handlers.Command
{
    [MicroService(ServiceNames.Sites)]
    public class RemoveSiteResource : ICommand
    {
        private Guid ResourceId { get; }

        [JsonConstructor]
        public RemoveSiteResource(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
