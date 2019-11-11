using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Tests.Mocks.Messaging
{
    public class MockRejectedEvent : IRejectedEvent
    {
        public string Code { get; set; }
        public string Reason { get; set; }
    }
}
