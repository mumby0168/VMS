using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Events;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Command
{
    public class UpdateEntryOrderHandler : ICommandHandler<UpdateEntryOrder>
    {
        private readonly IVmsLogger<UpdateEntryOrderHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly IDataSpecificationRepository _repository;

        public UpdateEntryOrderHandler(IVmsLogger<UpdateEntryOrderHandler> logger, IServiceBusMessagePublisher publisher, IDataSpecificationRepository repository)
        {
            _logger = logger;
            _publisher = publisher;
            _repository = repository;
        }


        public async Task HandleAsync(UpdateEntryOrder message, IRequestInfo requestInfo)
        {
            var entries = await _repository.GetEntriesAsync(message.BusinessId);

            var specifications = entries.OrderBy(s => s.Order).ToList();

            var updating = specifications.FirstOrDefault(d => d.Id == message.EntryId);
            if (updating is null)
            {
                _logger.LogWarning($"No entry found with ID: {message.EntryId}");
                _publisher.PublishEvent(new EntryOrderUpdateRejected(Codes.InvalidId, $"Entry with the id {message.EntryId} could not be found."), requestInfo);
                return;
            }

            var replacing = specifications.FirstOrDefault(s => s.Order == message.Order);
            int oldOrder = updating.Order;
            updating.UpdateOrder(message.Order);
            replacing.UpdateOrder(oldOrder);

            await _repository.UpdateAsync(updating);
            await _repository.UpdateAsync(replacing);

            _publisher.PublishEvent(new EntryOrderUpdated(), requestInfo);
        }
    }
}
