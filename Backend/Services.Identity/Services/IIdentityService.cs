using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using Services.Identity.Messages.Commands;
using Services.Identity.Models;

namespace Services.Identity.Services
{
    public interface IIdentityService
    {
        Task<IAuthToken> SignIn(string email, string password, string role);

        Task CreateAdmin(string email);

        Task CompleteAdmin(Guid code, string password, string passwordMatch, string email);
    }
}
    