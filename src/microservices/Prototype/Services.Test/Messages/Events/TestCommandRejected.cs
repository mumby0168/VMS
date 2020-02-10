using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Test.Messages.Events
{
    public class TestCommandRejected : IRejectedEvent
    {
        public TestCommandRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
        public string Code { get; }
        public string Reason { get; }
    }
}
