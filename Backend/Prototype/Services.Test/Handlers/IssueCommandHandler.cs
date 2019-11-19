using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Test.Messages.Commands;

namespace Services.Test.Handlers
{
    public class IssueCommandHandler : ICommandHandler<IssueCommand>
    {
        private readonly ILogger<IssueCommandHandler> _logger;

        public IssueCommandHandler(ILogger<IssueCommandHandler> logger)
        {
            _logger = logger;
        }
        public Task HandleAsync(IssueCommand message, IRequestInfo requestInfo)
        {
            _logger.LogInformation("Got issue command with message " + message.Message ?? "NO MESSAGE");
            return Task.CompletedTask;
        }
    }
}
