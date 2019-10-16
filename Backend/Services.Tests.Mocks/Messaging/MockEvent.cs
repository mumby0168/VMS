using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Tests.Mocks.Messaging
{
    [MicroService("test")]
    public class MockEvent : IEvent
    {
    }
}
