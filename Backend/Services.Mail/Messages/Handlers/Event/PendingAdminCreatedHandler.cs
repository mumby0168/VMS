using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Mail.Messages.Events;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Handlers.Event
{
    public class PendingAdminCreatedHandler : IEventHandler<PendingAdminCreated>
    {
        private readonly ILogger<PendingAdminCreatedHandler> _logger;

        public PendingAdminCreatedHandler(ILogger<PendingAdminCreatedHandler> logger)
        {
            _logger = logger;
        }
        
        public Task HandleAsync(PendingAdminCreated message, IRequestInfo requestInfo)
        {
            _logger.LogInformation(message.Email);
            return  Task.CompletedTask;
        }
    }
}
