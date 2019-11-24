using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Services.Common.Queries;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Sites.Factories;

namespace Services.Sites
{
    internal static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISiteServiceFactory, SiteServiceFactory>();
            return serviceCollection;
        }
    }
}
