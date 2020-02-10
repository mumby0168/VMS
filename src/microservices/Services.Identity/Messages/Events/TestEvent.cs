using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Events
{
    public class TestEvent : IRejectedEvent
    {
        public string Name { get; }

        public TestEvent(string name, string code, string reason)
        {
            Name = name;
            Code = code;
            Reason = reason;
        }

        public string Code { get; }
        public string Reason { get; }
    }
}
