using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Messages.Events.Send.Rejected
{
    public class SiteResourceRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public SiteResourceRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
