using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Factories
{
    public class UsersFactory : IUsersFactory
    {
        public IUser CreateUser(string firstName, string secondName, string email, string phoneNumber, string businessPhoneNumber,
            Guid basedSiteId, Guid businessId, Guid accountId) =>
            new User().Setup(firstName, secondName, email, phoneNumber, businessPhoneNumber, basedSiteId, businessId, accountId);

        public IAccount CreateAccount(Guid id, string email, int code)
        {
            return new Account().Create(id, email, code);
        }
    }
}
