using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Services
{
    public interface IRefreshTokenService
    {
        Task<string> CreateRefreshToken(string email);
    }
}
