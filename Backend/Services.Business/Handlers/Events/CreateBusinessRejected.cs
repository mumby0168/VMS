using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Businesses.Handlers.Events
{
    public class CreateBusinessRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public CreateBusinessRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
