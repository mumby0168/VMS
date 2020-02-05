using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Commands
{
    public class UpdateEntryOrder : ICommand
    {
        public Guid BusinessId { get; }

        public Guid EntryId { get; }

        public int Order { get; }

        [JsonConstructor]
        public UpdateEntryOrder(Guid businessId, Guid entryId, int order)
        {
            BusinessId = businessId;
            EntryId = entryId;
            Order = order;
        }
    }
}
