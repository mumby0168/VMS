using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Test.Messages.Events
{
    public class TestCommandPassed : IEvent
    {
        public string HappyMessage { get; }

        [JsonConstructor]
        public TestCommandPassed(string happyMessage)
        {
            HappyMessage = happyMessage;
        }
    }
}
