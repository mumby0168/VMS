using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Messages.Events.Send.Rejected
{
    public class SiteUpdateRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public SiteUpdateRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
