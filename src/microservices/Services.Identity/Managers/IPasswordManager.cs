using Services.Identity.Models;

namespace Services.Identity.Managers
{
    public interface IPasswordManager
    {
        IPassword EncryptPassword(string password);

        bool IsPasswordCorrect(string password, byte[] hash, byte[] salt);
    }
}
