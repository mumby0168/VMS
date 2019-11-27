using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Exceptions;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Sites.Domain;
using Services.Sites.Factories;
using Services.Sites.Messages.Commands;
using Services.Sites.Messages.Events.Send;
using Services.Sites.Messages.Events.Send.Rejected;
using Services.Sites.Repositorys;

namespace Services.Sites.Handlers.Command
{
    public class CreateSiteResourceHandler : ICommandHandler<CreateSiteResource>
    {
        private readonly ILogger<CreateSiteResourceHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly ISiteServiceFactory _factory;
        private readonly ISiteResourceRepository _siteResourceRepository;
        private readonly ISiteRepository _siteRepository;

        public CreateSiteResourceHandler(ILogger<CreateSiteResourceHandler> logger, IServiceBusMessagePublisher publisher, ISiteServiceFactory factory, ISiteResourceRepository siteResourceRepository, ISiteRepository siteRepository)
        {
            _logger = logger;
            _publisher = publisher;
            _factory = factory;
            _siteResourceRepository = siteResourceRepository;
            _siteRepository = siteRepository;
        }

        public async Task HandleAsync(CreateSiteResource message, IRequestInfo requestInfo)
        {
            if (!await _siteRepository.IsSiteIdValid(message.SiteId))
            {
                _publisher.PublishEvent(new SiteResourceRejected(Codes.InvalidSiteId, "The site in which to add the resource could not be found."), requestInfo);
                return;
            }

            ISiteResource resource = null;
            try
            {
                resource = _factory.CreateSiteResource(message.SiteId, message.Name, message.Identifier);
            }
            catch (VmsException e)
            {
                _publisher.PublishEvent(new SiteResourceRejected(e.Code, e.Message), requestInfo);
            }

            await _siteResourceRepository.AddAsync(resource);
            _publisher.PublishEvent(new SiteResourceCreated(), requestInfo);
        }
    }
}
