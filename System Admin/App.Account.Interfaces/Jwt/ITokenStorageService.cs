using Account.Interfaces.Models;

namespace Account.Interfaces.Jwt
{
    public interface ITokenStorageService
    {
         void SaveToken(string token);

         IJwtToken Token { get; }
    }
}