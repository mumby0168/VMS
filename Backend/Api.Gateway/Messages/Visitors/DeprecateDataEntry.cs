using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Visitors
{
    [MicroService(Services.Common.Names.Services.Visitors)]
    public class DeprecateDataEntry : ICommand
    {
        public Guid Id { get; }

        public Guid BusinessId { get; }

        [JsonConstructor]
        public DeprecateDataEntry(Guid id, Guid businessId)
        {
            Id = id;
            BusinessId = businessId;
        }
    }
}
