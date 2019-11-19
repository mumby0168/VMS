using System;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Business
{
    public class UpdateHeadContact : ICommand
    {
        public Guid BusinessId { get; }
        public string FirstName { get;}
        public string SecondName { get; }

        public string Email { get; }

        public string ContactNumber { get; }

        [JsonConstructor]
        public UpdateHeadContact(Guid businessId, string firstName, string secondName, string email, string contactNumber)
        {
            BusinessId = businessId;
            FirstName = firstName;
            SecondName = secondName;
            Email = email;
            ContactNumber = contactNumber;
        }
    }
}
