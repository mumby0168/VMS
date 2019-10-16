using System.Threading.Tasks;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public interface IQueryHandler<in TQuery, TReturns> where TQuery : IQuery
    {
        Task<TReturns> HandleAsync(TQuery query);
    }
}
