using System;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Sites
{
    [MicroService(ServiceNames.Sites)]
    public class UpdateSiteDetails : ICommand
    {
        public Guid SiteId { get; }

        public string Name { get; }

        public string AddressLine1 { get; }

        public string AddressLine2 { get; }

        public string PostCode { get; }

        [JsonConstructor]
        public UpdateSiteDetails(Guid siteId, string name, string addressLine1, string addressLine2, string postCode)
        {
            SiteId = siteId;
            Name = name;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostCode = postCode;
        }
    }
}
