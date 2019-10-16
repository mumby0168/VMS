using System.Threading.Tasks;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public interface IDispatcher
    {
        Task DispatchCommand<TCommand>(TCommand message, IRequestInfo requestInfo) where TCommand : ICommand;

        Task<TReturns> DispatchQuery<TReturns, TQuery>(TQuery query) where TQuery : IQuery;
    }
}
