using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Users.Domain
{
    public interface IUser : IDomain
    {
        string FirstName { get; }

        string SecondName { get; }

        string Email { get; }

        string PhoneNumber { get; }

        string BusinessPhoneNumber { get; }

        Guid BasedSiteId { get; }

        Guid BusinessId { get; }

        Guid AccountId { get; }

        IUser Setup(string firstName, string secondName, string email, string phoneNumber, string businessPhoneNumber,
            Guid basedSiteId, Guid businessId, Guid accountId);
    }
}
