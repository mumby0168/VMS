using System;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Messages.Commands
{
    public class CreateSiteResource : ICommand
    {
        public Guid SiteId { get; }
        public string Identifier { get; }

        public string Name { get; }

        [JsonConstructor]
        public CreateSiteResource(Guid siteId, string identifier, string name)
        {
            SiteId = siteId;
            Identifier = identifier;
            Name = name;
        }

        public CreateSiteResource()
        {
            
        }
    }
}
