using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Tests.Mocks.Messaging
{
    [MicroService("test")]
    public class MockCommand : ICommand
    {
    }
}
