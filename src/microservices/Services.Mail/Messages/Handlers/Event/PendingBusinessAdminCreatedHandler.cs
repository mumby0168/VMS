using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Mail.Messages.Events;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Handlers.Event
{
    public class PendingBusinessAdminCreatedHandler : IEventHandler<PendingBusinessAdminCreated>
    {
        private readonly ILogger<PendingBusinessAdminCreatedHandler> _logger;

        public PendingBusinessAdminCreatedHandler(ILogger<PendingBusinessAdminCreatedHandler> logger)
        {
            _logger = logger;
        }
        public Task HandleAsync(PendingBusinessAdminCreated message, IRequestInfo requestInfo)
        {
            _logger.LogInformation($"Email sent with code: {message.Code}");
            return Task.CompletedTask;
        }
    }
}
