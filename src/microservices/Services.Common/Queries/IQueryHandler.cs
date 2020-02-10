using System.Threading.Tasks;

namespace Services.Common.Queries
{
    public interface IQueryHandler<in TQuery, TReturns> where TQuery : IQuery
    {
        Task<TReturns> HandleAsync(TQuery query);
    }
}
