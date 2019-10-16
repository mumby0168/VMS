using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    public interface IServiceBusMessagePublisher
    {
        void PublishCommand<T>(ServiceBusMessageBase<T> message) where T : ICommand;

        void PublishEvent<T>(ServiceBusMessageBase<T> message) where T : IEvent;
    }
}
