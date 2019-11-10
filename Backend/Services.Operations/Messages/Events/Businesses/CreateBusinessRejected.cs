using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Businesses
{
    [MicroService(ServiceNames.Businesses)]
    public class CreateBusinessRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public CreateBusinessRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
