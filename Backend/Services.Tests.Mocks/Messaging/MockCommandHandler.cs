using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Tests.Mocks.Messaging
{
    public class MockCommandHandler : ICommandHandler<MockCommand>
    {
        public Task HandleAsync(MockCommand message, IRequestInfo requestInfo)
        {
            return Task.CompletedTask;
        }
    }
}
