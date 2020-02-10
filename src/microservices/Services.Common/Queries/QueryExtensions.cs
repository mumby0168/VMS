using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Common.Queries
{
    public static class QueryExtensions
    {
        public static IServiceCollection AddQuerySupport(this IServiceCollection services) =>
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
    }
}
