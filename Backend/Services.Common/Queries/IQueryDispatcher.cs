using System.Threading.Tasks;

namespace Services.Common.Queries
{
    public interface IQueryDispatcher
    {
        Task<TReturns> Dispatch<TQuery, TReturns>(TQuery query) where TQuery : IQuery;
    }
}
