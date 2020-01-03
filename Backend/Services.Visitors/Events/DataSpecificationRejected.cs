﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Events
{
    public class DataSpecificationRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public DataSpecificationRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
