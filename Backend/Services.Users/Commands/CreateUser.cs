using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Users.Commands
{
    public class CreateUser : ICommand
    {
        public Guid AccountId { get; }
        public string FirstName { get; }

        public string SecondName { get; }

        public string PhoneNumber { get; }

        public string BusinessPhoneNumber { get; }

        public Guid BasedSiteId { get; }

        public Guid BusinessId { get; }

        [JsonConstructor]
        public CreateUser(string firstName, string secondName, string phoneNumber, string businessPhoneNumber, Guid basedSiteId, Guid businessId, Guid accountId)
        {
            FirstName = firstName;
            SecondName = secondName;
            PhoneNumber = phoneNumber;
            BusinessPhoneNumber = businessPhoneNumber;
            BasedSiteId = basedSiteId;
            BusinessId = businessId;
            AccountId = accountId;
        }
    }
}
