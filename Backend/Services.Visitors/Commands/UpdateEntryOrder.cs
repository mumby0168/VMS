using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Commands
{
    public class UpdateEntryOrder : ICommand
    {
        public Guid BusinessId { get; }
        public IEnumerable<Entry> Entries { get; }

        [JsonConstructor]
        public UpdateEntryOrder(IEnumerable<Entry> entries, Guid businessId)
        {
            Entries = entries;
            BusinessId = businessId;
        }
    }

    public class Entry
    {
        public Guid Id { get; }

        public int Order { get; }

        [JsonConstructor]
        public Entry(Guid id, int order)
        {
            Id = id;
            Order = order;
        }
    }
}
