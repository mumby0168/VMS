using System.Threading.Tasks;
using MongoDB.Driver.Core.Operations;

namespace Services.Mail.Messages.Mail
{
    public interface IMailManager
    {
        Task SendAsync(string subject, string content, string to);
    }
}