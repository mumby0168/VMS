using System.Threading.Tasks;
namespace System_Admin.Clients.Account
{
    public interface IAccountClient
    {
         Task<bool> SignIn(string email, string password);
    }
}