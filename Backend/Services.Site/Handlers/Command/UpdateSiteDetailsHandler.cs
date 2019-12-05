using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Sites.Messages.Commands;
using Services.Sites.Messages.Events.Send;
using Services.Sites.Messages.Events.Send.Rejected;
using Services.Sites.Repositorys;

namespace Services.Sites.Handlers.Command
{
    public class UpdateSiteDetailsHandler : ICommandHandler<UpdateSiteDetails>
    {
        private readonly IVmsLogger<UpdateSiteDetailsHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly ISiteRepository _siteRepository;

        public UpdateSiteDetailsHandler(IVmsLogger<UpdateSiteDetailsHandler> logger, IServiceBusMessagePublisher publisher,
            ISiteRepository siteRepository)
        {
            _logger = logger;
            _publisher = publisher;
            _siteRepository = siteRepository;
        }

        public async Task HandleAsync(UpdateSiteDetails message, IRequestInfo requestInfo)
        {
            var site = await _siteRepository.GetAsync(message.SiteId);
            if (site is null)
            {
                _logger.LogInformation($"Site with id: {message.SiteId} could not be found");
                _publisher.PublishEvent(
                    new SiteUpdateRejected(Codes.InvalidId, "The site cannot be retrieved to update."), requestInfo);
                return;
            }

            try
            {
                site.Update(message.Name, message.PostCode, message.AddressLine1, message.AddressLine2);
            }
            catch (VmsException e)
            {
                _logger.LogInformation($"Update failed with code: {e.Code} for reason: {e.Message}");
                _publisher.PublishEvent(new SiteUpdateRejected(e.Code, e.Message), requestInfo);
                return;
            }

            await _siteRepository.Update(site);
            _publisher.PublishEvent(new SiteUpdated(), requestInfo);
        }
    }
}

