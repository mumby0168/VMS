using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<string> SignIn(string email, string password, string role)
        {
            throw new NotImplementedException();
        }

        public Task CreateAdmin(string email)
        {
            throw new NotImplementedException();
        }

        public Task CompleteAdmin(Guid code, string password, string passwordMatch, string email)
        {
            throw new NotImplementedException();
        }
    }
}
