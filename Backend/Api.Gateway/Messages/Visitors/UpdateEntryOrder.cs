using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Visitors
{
    [MicroService(Services.Common.Names.Services.Visitors)]
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
