using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using Services.Identity.Messages.Commands;

namespace Services.Identity.Services
{
    public interface IIdentityService
    {
        Task<string> SignIn(string email, string password, string role);

        Task CreateAdmin(string email);

        Task CompleteAdmin(Guid code, string password, string passwordMatch, string email);
    }
}
    