using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Business.Messages.Events.Rejected
{
    public class UpdateBusinessRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public UpdateBusinessRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
