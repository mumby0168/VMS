using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Sites.Messages.Commands;

namespace Services.Sites.Handlers.Command
{
    public class CreateSiteResourceHandler : ICommandHandler<CreateSiteResource>
    {
        private readonly ILogger<CreateSiteResourceHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;

        public CreateSiteResourceHandler(ILogger<CreateSiteResourceHandler> logger, IServiceBusMessagePublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        public Task HandleAsync(CreateSiteResource message, IRequestInfo requestInfo)
        {
            throw new NotImplementedException();
        }
    }
}
