using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Messages.Events.Send.Rejected
{
    [MicroService(ServiceNames.Sites)]
    public class SiteUpdateRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public SiteUpdateRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
