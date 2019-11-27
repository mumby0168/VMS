using System;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Messages.Commands
{
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
