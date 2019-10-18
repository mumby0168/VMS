using System_Admin.Areas.Account.Models;

namespace System_Admin.Services
{
    public interface ITokenStorageService
    {
         void SaveToken(string token);

         IJwtToken Token { get; }
    }
}