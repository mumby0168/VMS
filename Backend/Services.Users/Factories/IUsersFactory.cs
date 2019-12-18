using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Factories
{
    public interface IUsersFactory
    {
        IUser CreateUser(string firstName, string secondName, string email, string phoneNumber, string businessPhoneNumber,
            Guid basedSiteId, Guid businessId, Guid accountId);

        IAccount CreateAccount(Guid id, string email);
    }
}
