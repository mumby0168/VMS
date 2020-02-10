using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Attributes;

namespace Services.Tests.Mocks.Messaging
{
    [MicroService("test")]
    public class MockCommandWithAttribute
    {
    }
}
