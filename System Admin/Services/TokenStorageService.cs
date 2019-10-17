using System_Admin.Areas.Account.Models;

namespace System_Admin.Services
{
    public class TokenStorageService : ITokenStorageService
    {
        public IJwtToken Token { get; private set; }

        public void SaveToken(string token)
        {
            Token = JwtToken.Process(token);
        }
    }
}