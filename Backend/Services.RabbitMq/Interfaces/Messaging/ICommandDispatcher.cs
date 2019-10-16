using System.Threading.Tasks;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public interface ICommandDispatcher
    {
        Task Dispatch<TCommand>(TCommand message, IRequestInfo requestInfo) where TCommand : ICommand;
    }

}
