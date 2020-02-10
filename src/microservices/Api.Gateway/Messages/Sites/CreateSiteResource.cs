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
    [MicroService(Services.Common.Names.Services.Sites)]
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
    }
}
