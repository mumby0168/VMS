using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    public interface IServiceBusMessagePublisher
    {
        void PublishCommand<T>(T message, IRequestInfo requestInfo) where T : ICommand;

        void PublishEvent<T>(T message, IRequestInfo requestInfo) where T : IEvent;
    }
}
