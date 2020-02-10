using Account.Interfaces.Jwt;
using Account.Interfaces.Models;
using Account.Models;
using App.Shared.Context;

namespace Account.Jwt
{
    public class TokenStorageService : ITokenStorageService
    {
        private readonly IUserContext _context;

        public TokenStorageService(IUserContext context)
        {
            _context = context;
        }
        public IJwtToken Token { get; private set; }

        public void SaveToken(string token)
        {
            Token = JwtToken.Process(token);
            _context.Token = Token.RawToken;
            _context.Id = Token.Id;
            //TODO: This will be null.
            _context.Email = Token.Email;
            _context.IsLoggedIn = true;
        }

        public void RemoveToken()
        {
            Token = null;
            _context.Clear();
        }


    }
}