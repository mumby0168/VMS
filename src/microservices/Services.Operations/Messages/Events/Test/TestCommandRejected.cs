﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Test
{
    [MicroService("Services.Test")]
    public class TestCommandRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public TestCommandRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
