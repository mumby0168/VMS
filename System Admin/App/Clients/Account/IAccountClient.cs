using System.Threading.Tasks;
namespace Manager.Clients.Account
{
    public interface IAccountClient
    {
         Task<bool> SignIn(string email, string password);
    }
}