using System;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Sagas.Commands
{
    public class CreateUser : ICommand
    {
        public string FirstName { get; }

        public string SecondName { get; }

        public string PhoneNumber { get; }

        public string BusinessPhoneNumber { get; }

        public Guid BasedSiteId { get; }

        public Guid BusinessId { get; }

        public CreateUser(string firstName, string secondName, string phoneNumber, string businessPhoneNumber, Guid basedSiteId, Guid businessId)
        {
            FirstName = firstName;
            SecondName = secondName;
            PhoneNumber = phoneNumber;
            BusinessPhoneNumber = businessPhoneNumber;
            BasedSiteId = basedSiteId;
            BusinessId = businessId;
        }
    }
}
