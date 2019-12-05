
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Sites.Factories;
using Services.Sites.Messages.Commands;
using Services.Sites.Messages.Events.Send;
using Services.Sites.Messages.Events.Send.Rejected;
using Services.Sites.Repositorys;

namespace Services.Sites.Handlers.Command
{
    public class CreateSiteHandler : ICommandHandler<CreateSite>
    {
        private readonly IVmsLogger<CreateSiteHandler> _logger;
        private readonly ISiteServiceFactory _factory;
        private readonly ISiteRepository _siteRepository;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly IBusinessRepository _businessRepository;

        public CreateSiteHandler(IVmsLogger<CreateSiteHandler> logger, ISiteServiceFactory factory, ISiteRepository siteRepository, IServiceBusMessagePublisher publisher, IBusinessRepository businessRepository)
        {
            _logger = logger;
            _factory = factory;
            _siteRepository = siteRepository;
            _publisher = publisher;
            _businessRepository = businessRepository;
        }

        public async Task HandleAsync(CreateSite message, IRequestInfo requestInfo)
        {
            if (!await _businessRepository.IsBusinessValidAsync(message.BusinessId))
            {
                _publisher.PublishEvent(new CreateSiteRejected(Codes.InvalidBusinessId, "The business could not be found to create a site for."), requestInfo);
                return;
            }

            try
            {
                var contact = _factory.CreateContact(message.FirstName, message.SecondName,
                    message.Email, message.ContactNumber);
                var site = _factory.CreateSite(message.BusinessId, message.Name, message.PostCode, message.AddressLine1,
                    message.AddressLine2, contact);

                await _siteRepository.AddAsync(site);
                _publisher.PublishEvent(new SiteCreated(site.Id), requestInfo);
            }
            catch (VmsException e)
            {
                _publisher.PublishEvent(new CreateSiteRejected(e.Code, e.Message), requestInfo);
            }
        }
    }
}
