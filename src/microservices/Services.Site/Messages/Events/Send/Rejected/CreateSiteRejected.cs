using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Messages.Events.Send.Rejected
{
    public class CreateSiteRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public CreateSiteRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
