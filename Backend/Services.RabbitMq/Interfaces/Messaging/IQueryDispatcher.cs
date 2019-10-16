using System.Threading.Tasks;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public interface IQueryDispatcher
    {
        Task<TReturns> Dispatch<TQuery, TReturns>(TQuery query) where TQuery : IQuery;
    }
}
