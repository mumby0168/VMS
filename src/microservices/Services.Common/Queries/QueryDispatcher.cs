using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Common.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task<TReturns> Dispatch<TQuery, TReturns>(TQuery query) where TQuery : IQuery
        {
            var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TReturns>>();
            return handler.HandleAsync(query);
        }
    }
}
