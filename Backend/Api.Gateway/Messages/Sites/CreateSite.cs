using System;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Sites
{
    public class CreateSite : ICommand
    {
        public Guid BusinessId { get; }
        public string Name { get; }
        public string PostCode { get; }
        public string AddressLine1 { get; }
        public string AddressLine2 { get; }

        public string FirstName { get; }


        public string SecondName { get; }


        public string ContactNumber { get; }


        public string Email { get; }


        [JsonConstructor]
        public CreateSite(string name, string postCode, string addressLine1, string addressLine2, string firstName, string secondName, string contactNumber, string email, Guid businessId)
        {
            Name = name;
            PostCode = postCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            FirstName = firstName;
            SecondName = secondName;
            ContactNumber = contactNumber;
            Email = email;
            BusinessId = businessId;
        }

        public CreateSite()
        {
            
        }
    }
}
