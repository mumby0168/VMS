using System;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Sites
{
    [MicroService(ServiceNames.Sites)]
    public class UpdateSiteContact : ICommand
    {
        public Guid SiteId { get; }
        public string FirstName { get; }

        public string SecondName { get; }

        public string Email { get; }

        public string ContactNumber { get; }

        [JsonConstructor]
        public UpdateSiteContact(Guid siteId, string firstName, string secondName, string email, string contactNumber)
        {
            SiteId = siteId;
            FirstName = firstName;
            SecondName = secondName;
            Email = email;
            ContactNumber = contactNumber;
        }
    }
}
