using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Sites.Domain;
using Services.Sites.Messages.Events;
using Services.Sites.Repositorys;

namespace Services.Sites.Handlers.Events
{
    public class BusinessCreatedHandler : IEventHandler<BusinessCreated>
    {
        private readonly IVmsLogger<BusinessCreatedHandler> _logger;
        private readonly IBusinessRepository _repository;

        public BusinessCreatedHandler(IVmsLogger<BusinessCreatedHandler> logger, IBusinessRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task HandleAsync(BusinessCreated message, IRequestInfo requestInfo)
        {
            var business = new Business().Create(message.Id);
            await _repository.AddAsync(business);
            _logger.LogInformation("Business added to Site Service with id:" + business.Id);
        }
    }
}
