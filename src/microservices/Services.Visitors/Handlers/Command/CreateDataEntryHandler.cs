using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
using Services.Visitors.Domain.Aggregate;
using Services.Visitors.Domain.Domain.Specification;
using Services.Visitors.Events;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Command
{
    public class CreateDataEntryHandler : ICommandHandler<CreateDataEntry>
    {
        private readonly IVmsLogger<CreateDataEntryHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly ISpecificationAggregate _aggregate;
        private readonly ISpecificationRepository _repository;

        public CreateDataEntryHandler(IVmsLogger<CreateDataEntryHandler> logger, IServiceBusMessagePublisher publisher, ISpecificationAggregate aggregate, ISpecificationRepository repository)
        {
            _logger = logger;
            _publisher = publisher;
            _aggregate = aggregate;
            _repository = repository;
        }


        public async Task HandleAsync(CreateDataEntry message, IRequestInfo requestInfo)    
        {
            SpecificationDocument specificationDocument;
            var order = await _repository.GetNextOrderNumberAsync(message.BusinessId);
            try
            {
                specificationDocument =
                    _aggregate.Create(message.Label, order, message.ValidationMessage, message.ValidationCode, message.BusinessId);
            }
            catch (VmsException e)
            {
                _publisher.PublishEvent(new DataSpecificationRejected(e.Code, e.Message), requestInfo);
                _logger.LogWarning(e.Message, LoggingCategories.DomainValidation);
                return;
            }

            await _repository.AddAsync(specificationDocument);
            _publisher.PublishEvent(new DataSpecificationCreated(), requestInfo);
            _logger.LogInformation($"Data specification labeled {message.Label} created with id: {specificationDocument.Id}");
        }
    }
}
