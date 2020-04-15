using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Test.Messages.Commands;
using Services.Test.Messages.Events;

namespace Services.Test.Handlers
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        private readonly IServiceBusMessagePublisher _serviceBusMessagePublisher;

        public TestCommandHandler(IServiceBusMessagePublisher serviceBusMessagePublisher)
        {
            _serviceBusMessagePublisher = serviceBusMessagePublisher;
        }

        public Task HandleAsync(TestCommand message, IRequestInfo requestInfo)
        {
            if (message.IsPass)
            {
                _serviceBusMessagePublisher.PublishEvent(new TestCommandPassed("This is a happy message"), requestInfo);
            }
            else
            {
                _serviceBusMessagePublisher.PublishEvent(new TestCommandRejected("failed", "The parameter passed was false."), requestInfo);
            }

            return Task.CompletedTask;
        }
    }
}
