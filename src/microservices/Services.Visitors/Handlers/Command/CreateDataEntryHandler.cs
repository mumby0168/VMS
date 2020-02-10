using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Events;
using Services.Visitors.Factories;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Command
{
    public class CreateDataEntryHandler : ICommandHandler<CreateDataEntry>
    {
        private readonly IVmsLogger<CreateDataEntryHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly IDataSpecificationFactory _factory;
        private readonly IDataSpecificationRepository _repository;

        public CreateDataEntryHandler(IVmsLogger<CreateDataEntryHandler> logger, IServiceBusMessagePublisher publisher, IDataSpecificationFactory factory, IDataSpecificationRepository repository)
        {
            _logger = logger;
            _publisher = publisher;
            _factory = factory;
            _repository = repository;
        }


        public async Task HandleAsync(CreateDataEntry message, IRequestInfo requestInfo)
        {
            IDataSpecification specification = null;
            var order = await _repository.GetNextOrderNumberAsync(message.BusinessId);

            try
            {
                specification =
                    _factory.Create(message.Label, order, message.ValidationMessage, message.ValidationCode, message.BusinessId);
            }
            catch (VmsException e)
            {
                _publisher.PublishEvent(new DataSpecificationRejected(e.Code, e.Message), requestInfo);
                _logger.LogWarning(e.Message, LoggingCategories.DomainValidation);
                return;
            }

            await _repository.AddAsync(specification);
            _publisher.PublishEvent(new DataSpecificationCreated(), requestInfo);
            _logger.LogInformation($"Data specification labeled {message.Label} created with id: {specification.Id}");
        }
    }
}
