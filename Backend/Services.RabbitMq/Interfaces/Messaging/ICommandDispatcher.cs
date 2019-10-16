using System.Threading.Tasks;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public interface ICommandDispatcher
    {
        Task Dispatch<TCommand>(ServiceBusMessageBase<TCommand> message) where TCommand : ICommand;
    }

}
