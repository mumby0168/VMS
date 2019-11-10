using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.Businesses.Repositorys;

namespace Services.Businesses
{
    internal static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBusinessRepository, BusinessRepository>();
            return serviceCollection;
        }
    }
}
