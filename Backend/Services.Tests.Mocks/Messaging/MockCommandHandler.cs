using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Tests.Mocks.Messaging
{
    public class MockCommandHandler : ICommandHandler<MockCommand>
    {
        public virtual Task HandleAsync(ServiceBusMessageBase<MockCommand> messageBase)
        {
            return Task.CompletedTask;
        }
    }
}
