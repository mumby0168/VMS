using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Logging;
using Services.Common.Mongo;
using Services.Identity.Domain;
using Services.Identity.Messages.Events.Subscribed;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Handlers
{
    public class BusinessCreatedHandler : IEventHandler<BusinessCreated>
    {
        private readonly IVmsLogger<BusinessCreatedHandler> _logger;
        private readonly IMongoRepository<Business> _repository;

        public BusinessCreatedHandler(IVmsLogger<BusinessCreatedHandler> logger, IMongoRepository<Business> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task HandleAsync(BusinessCreated message, IRequestInfo requestInfo)
        {
            await _repository.AddAsync(new Business(message.Id));
            _logger.LogInformation($"Business with id: {message.Id} stored in identity service");
        }
    }
}
