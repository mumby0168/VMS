using Account.Interfaces.Jwt;
using Account.Interfaces.Models;
using Account.Models;

namespace Account.Jwt
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