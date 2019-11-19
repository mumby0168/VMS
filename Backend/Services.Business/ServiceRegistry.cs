using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Services.Business.Dtos;
using Services.Business.Factories;
using Services.Business.Handlers.Command;
using Services.Business.Handlers.Query;
using Services.Business.Messages.Commands;
using Services.Business.Messages.Queries;
using Services.Business.Repositorys;
using Services.Common.Queries;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Business
{
    internal static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBusinessRepository, BusinessRepository>();
            serviceCollection.AddTransient<IBusinessesFactory, BusinessesFactory>();

            //command handlers
            serviceCollection.AddTransient<ICommandHandler<CreateBusiness>, CreateBusinessHandler>();
            serviceCollection.AddTransient<ICommandHandler<UpdateBusinessDetails>, UpdateBusinessDetailsHandler>();
            serviceCollection.AddTransient<ICommandHandler<UpdateHeadContact>, UpdateHeadContactHandler>();
            serviceCollection.AddTransient<ICommandHandler<UpdateHeadOffice>, UpdateHeadOfficeHandler>();

            //query handlers
            serviceCollection
                .AddTransient<IQueryHandler<BusinessesSummary, IEnumerable<BusinessSummaryDto>>, BusinessesSummaryHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetBusiness, BusinessDto>, GetBusinessQueryHandler>();
            return serviceCollection;
        }
    }
}
