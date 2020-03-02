using Microsoft.Extensions.DependencyInjection;
using Services.Common.Mongo;
using Services.Visitors.Domain.Aggregate;
using Services.Visitors.Domain.Domain.Visitor;

namespace Services.Visitors.Domain
{
    public static class VisitorDomainExtensions
    {
        public static IServiceCollection AddVisitorDomain(this IServiceCollection services)
        {
            services.AddMongo()
                .AddMongoCollection<VisitorDocument>();
            services.AddTransient<IVisitorAggregate, VisitorAggregate>();
            
            return services;
        }
        
    }
}