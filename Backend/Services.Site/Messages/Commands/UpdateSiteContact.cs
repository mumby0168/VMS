using System;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Messages.Commands
{ 
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

        public UpdateSiteContact()
        {
            
        }
    }
}
