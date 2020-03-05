using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Domain.Aggregate;
using Services.Visitors.Events;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Command
{
    public class DeprecateDataEntryHandler : ICommandHandler<DeprecateDataEntry>
    {
        private readonly IVmsLogger<DeprecateDataEntryHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly ISpecificationRepository _repository;
        private readonly ISpecificationAggregate _specificationAggregate;

        public DeprecateDataEntryHandler(IVmsLogger<DeprecateDataEntryHandler> logger,IServiceBusMessagePublisher publisher, ISpecificationRepository repository, ISpecificationAggregate specificationAggregate)
        {
            _logger = logger;
            _publisher = publisher;
            _repository = repository;
            _specificationAggregate = specificationAggregate;
        }

        public async Task HandleAsync(DeprecateDataEntry message, IRequestInfo requestInfo)
        {
            var entries = await _repository.GetEntriesAsync(message.BusinessId);
            var dataSpecifications = entries.ToList();

            if (!dataSpecifications.Any())
            {
                _logger.LogWarning($"No entries found with business ID: {message.BusinessId}");
                _publisher.PublishEvent(new DataEntryDeprecationRejected(Codes.InvalidBusinessId, $"Entries with the id {message.BusinessId} could not be found."), requestInfo);
                return;
            }

            var spec = dataSpecifications.FirstOrDefault(d => d.Id == message.Id);
            if (spec is null)
            {
                _logger.LogWarning($"No entry found with ID: {message.Id}");
                _publisher.PublishEvent(new DataEntryDeprecationRejected(Codes.InvalidId, $"Entry with the id {message.Id} could not be found."), requestInfo);
                return;
            }
            
            if (spec.IsMandatory)
            {
                _logger.LogInformation($"{spec.Label} is  set as mandatory and cannot be deprecated.");
                _publisher.PublishEvent(new DataEntryDeprecationRejected(Codes.InvalidId, $"{spec.Label} is  set as mandatory and cannot be deprecated."), requestInfo);
                return;
            }

            _specificationAggregate.Deprecate(spec);
            await _repository.UpdateAsync(spec);
            dataSpecifications.Remove(spec);
            dataSpecifications = dataSpecifications.Where(s => s.IsMandatory == false).ToList();

            var ordered = dataSpecifications.OrderBy(d => d.Order).ToList();
            for (int i = 0; i < ordered.Count; i++)
            {
                _specificationAggregate.UpdateOrder(ordered[i], i + 1);
                await _repository.UpdateAsync(ordered[i]);
            }

            _publisher.PublishEvent(new DataEntryDeprecated(), requestInfo);
            _logger.LogInformation($"Entry deprecated with id: {message.Id} and other entries re-ordered.");
        }
    }
}
