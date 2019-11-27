using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Sites.Messages.Commands;
using Services.Sites.Repositorys;

namespace Services.Sites.Handlers.Command
{
    public class RemoveSiteResourceHandler : ICommandHandler<RemoveSiteResource>
    {
        private readonly ILogger<RemoveSiteResourceHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly ISiteResourceRepository _repository;

        public RemoveSiteResourceHandler(ILogger<RemoveSiteResourceHandler> logger, IServiceBusMessagePublisher publisher, ISiteResourceRepository repository)
        {
            _logger = logger;
            _publisher = publisher;
            _repository = repository;
        }

        public async Task HandleAsync(RemoveSiteResource message, IRequestInfo requestInfo)
        {
            await _repository.RemoveAsync(message.ResourceId);
        }
    }
}
