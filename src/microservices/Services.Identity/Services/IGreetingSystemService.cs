using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Identity.Models;

namespace Services.Identity.Services
{
    public interface IGreetingSystemService
    {
        Task<IAuthToken> SignIn(string email, string password, int businessCode);
    }
}
