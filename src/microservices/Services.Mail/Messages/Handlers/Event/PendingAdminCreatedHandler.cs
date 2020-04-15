using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Mail.Config;
using Services.Mail.Messages.Events;
using Services.Mail.Messages.Mail;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Handlers.Event
{
    public class PendingAdminCreatedHandler : IEventHandler<PendingAdminCreated>
    {
        private readonly ILogger<PendingAdminCreatedHandler> _logger;
        private readonly IMailManager _mailManager;
        private readonly ClientsAddresses _clientsAddresses;

        public PendingAdminCreatedHandler(ILogger<PendingAdminCreatedHandler> logger, IMailManager mailManager, ClientsAddresses clientsAddresses)
        {
            _logger = logger;
            _mailManager = mailManager;
            _clientsAddresses = clientsAddresses;
        }
        
        public async Task HandleAsync(PendingAdminCreated message, IRequestInfo requestInfo)
        {
            await _mailManager.SendAsync("Account Setup", $@"Your account has been created to join the admin team. You can finish the setup for this account here: {_clientsAddresses.System}complete-admin. Please use the following code: {message.Code}", message.Email);
            _logger.LogInformation($"Email sent with code: {message.Code}");
        }
    }
}
