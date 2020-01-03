using System;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Users.Commands
{
    [MicroService(Services.Common.Names.Services.Users)]
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
