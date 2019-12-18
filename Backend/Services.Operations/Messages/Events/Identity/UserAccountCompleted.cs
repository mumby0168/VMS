using System;

namespace Services.Operations.Messages.Events.Identity
{
    public class UserAccountCompleted
    {
        public string FirstName { get; }

        public string SecondName { get; }

        public string PhoneNumber { get; }

        public string BusinessPhoneNumber { get; }

        public Guid BasedSiteId { get; }

        public Guid BusinessId { get; }

        public UserAccountCompleted(string firstName, string secondName, string phoneNumber, string businessPhoneNumber, Guid basedSiteId, Guid businessId)
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
