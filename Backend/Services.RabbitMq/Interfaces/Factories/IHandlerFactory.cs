using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Interfaces.Factories
{
    public interface IHandlerFactory
    {
        ICommandHandler<T> ResolveCommandHandler<T>() where T : ICommand;

        IEventHandler<T> ResolveEventHandler<T>() where T : IEvent;
    }
}
