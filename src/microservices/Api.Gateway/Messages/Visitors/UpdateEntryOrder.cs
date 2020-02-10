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
