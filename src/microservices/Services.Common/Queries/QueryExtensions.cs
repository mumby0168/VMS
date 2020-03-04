using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Common.Queries
{
    public static class QueryExtensions
    {
        public static IServiceCollection AddQuerySupport(this IServiceCollection services)
        {
            services.Scan(selector => selector.FromAssemblies(Assembly.GetEntryAssembly())
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))).AsImplementedInterfaces()
                .WithScopedLifetime());
            return services.AddTransient<IQueryDispatcher, QueryDispatcher>();
        }
    }
}
