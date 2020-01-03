using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
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

            var dataSpecifications = entries.ToList();

            if (dataSpecifications.Count != message.Entries.Count())
            {
                _publisher.PublishEvent(new EntryOrderUpdateRejected(Codes.InvalidAmount, "The collection provided does not contain all of the items."), requestInfo);
                return;
            }

            //TODO: Check the order when applied possibly. Purely API checks here client could deal with it.

            foreach (var entry in dataSpecifications)
            {
                var messageEntry = message.Entries.FirstOrDefault(e => e.Id == entry.Id);
                if(messageEntry is null) continue;
                entry.UpdateOrder(messageEntry.Order);
                await _repository.UpdateAsync(entry);
            }

            _publisher.PublishEvent(new EntryOrderUpdated(), requestInfo);
        }
    }
}
