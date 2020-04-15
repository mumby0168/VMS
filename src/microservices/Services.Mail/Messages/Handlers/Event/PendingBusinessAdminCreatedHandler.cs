using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Mail.Config;
using Services.Mail.Messages.Events;
using Services.Mail.Messages.Mail;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Handlers.Event
{
    public class PendingBusinessAdminCreatedHandler : IEventHandler<PendingBusinessAdminCreated>
    {
        private readonly ILogger<PendingBusinessAdminCreatedHandler> _logger;
        private readonly IMailManager _mailManager;
        private readonly ClientsAddresses _clientsAddresses;

        public PendingBusinessAdminCreatedHandler(ILogger<PendingBusinessAdminCreatedHandler> logger, IMailManager mailManager, ClientsAddresses clientsAddresses)
        {
            _logger = logger;
            _mailManager = mailManager;
            _clientsAddresses = clientsAddresses;
        }
        public async Task HandleAsync(PendingBusinessAdminCreated message, IRequestInfo requestInfo)
        {
            await _mailManager.SendAsync("Account Setup",
                $@"Your account has been setup for access to your visitor management system please complete your signup here: {_clientsAddresses.Portal}complete/{message.Code}",
                message.Email);
            
            _logger.LogInformation($"Email sent with code: {message.Code}");
        }
    }
}
