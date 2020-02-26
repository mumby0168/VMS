using System;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Commands
{
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
