using System.Threading.Tasks;
using Services.Common.Logging;
using Services.Mail.Config;
using Services.Mail.Messages.Events;
using Services.Mail.Messages.Mail;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Handlers.Event
{
    public class UserAccountCreatedHandler : IEventHandler<UserAccountCreated>
    {
        private readonly IVmsLogger<UserAccountCreatedHandler> _logger;
        private readonly IMailManager _mailManager;
        private readonly ClientsAddresses _clientsAddresses;

        public UserAccountCreatedHandler(IVmsLogger<UserAccountCreatedHandler> logger, IMailManager mailManager, ClientsAddresses clientsAddresses)
        {
            _logger = logger;
            _mailManager = mailManager;
            _clientsAddresses = clientsAddresses;
        }
        
        public async Task HandleAsync(UserAccountCreated message, IRequestInfo requestInfo)
        {
            await _mailManager.SendAsync("Account Setup", $@"Your account has been setup for access to your visitor management system please complete your signup here: {_clientsAddresses.Portal}complete/{message.Code}", message.Email);
            
            _logger.LogInformation($"Email sent to user to setup account with code: {message.Code}");
        }
    }
}