using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Users.Events;
using Services.Users.Factories;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Events
{
    public class SiteCreatedHandler : IEventHandler<SiteCreated>
    {
        private readonly IVmsLogger<SiteCreatedHandler> _logger;
        private readonly ISiteFactory _factory;
        private readonly ISiteRepository _siteRepository;

        public SiteCreatedHandler(IVmsLogger<SiteCreatedHandler> logger, ISiteFactory factory, ISiteRepository siteRepository)
        {
            _logger = logger;
            _factory = factory;
            _siteRepository = siteRepository;
        }

        public async Task HandleAsync(SiteCreated message, IRequestInfo requestInfo)
        {
            var site = _factory.CreateSite(message.Id, message.Name);
            await _siteRepository.AddSiteAsync(site);
            _logger.LogInformation($"Site created in users service with id: {site.Id}");
        }
    }
}
