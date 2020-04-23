using System.Threading.Tasks;
using Services.Common.Logging;
using Services.Mail.Messages.Events;
using Services.Mail.Messages.Mail;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Handlers.Event
{
    public class VisitorSignedInHandler : IEventHandler<VisitorSignedIn>
    {
        private readonly IVmsLogger<VisitorSignedInHandler> _logger;
        private readonly IMailManager _mailManager;

        public VisitorSignedInHandler(IVmsLogger<VisitorSignedInHandler> logger, IMailManager mailManager)
        {
            _logger = logger;
            _mailManager = mailManager;
        }
        public async Task HandleAsync(VisitorSignedIn message, IRequestInfo requestInfo)
        {
            await _mailManager.SendAsync($"{message.VisitorName} Arrived", $"{message.VisitorName} has arrived at {message.Site} {message.SignedInAt.ToShortDateString()} {message.SignedInAt.ToShortTimeString()}", message.VisitingEmail);
            _logger.LogInformation($"Visitor arrived email sent.");
        }
    }
}