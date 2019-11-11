using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.Businesses.Factories;
using Services.Businesses.Handlers.Command;
using Services.Businesses.Messages.Commands;
using Services.Businesses.Repositorys;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Businesses
{
    internal static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBusinessRepository, BusinessRepository>();
            serviceCollection.AddTransient<IBusinessesFactory, BusinessesFactory>();

            //command handlers
            serviceCollection.AddTransient<ICommandHandler<CreateBusiness>, CreateBusinessHandler>();
            return serviceCollection;
        }
    }
}
