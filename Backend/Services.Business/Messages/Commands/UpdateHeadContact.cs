using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Business.Messages.Commands
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
