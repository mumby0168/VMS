using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Tests.Mocks.Messaging
{
    public class MockEventHandler : IEventHandler<MockEvent>
    {

        public Task HandleAsync(MockEvent message, IRequestInfo requestInfo)
        {
            return Task.CompletedTask;
        }
    }
}
