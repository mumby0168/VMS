using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Users.Events
{
    public class AccessRecordRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public AccessRecordRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
