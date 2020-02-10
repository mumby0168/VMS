using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Push.Messages.Events
{
    [MicroService(Common.Names.Services.Operations)]
    public class OperationFailed : IEvent
    {
        [JsonConstructor]
        public OperationFailed(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
        public string Code { get; }

        public string Reason { get; }
    }
}
