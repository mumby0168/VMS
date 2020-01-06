using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Services.Common.Queries;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Sites.Domain;
using Services.Sites.Dtos;
using Services.Sites.Factories;
using Services.Sites.Handlers.Command;
using Services.Sites.Handlers.Events;
using Services.Sites.Handlers.Query;
using Services.Sites.Messages.Commands;
using Services.Sites.Messages.Events;
using Services.Sites.Messages.Queries;
using Services.Sites.Repositorys;
using Services.Sites.Services;

namespace Services.Sites
{
    internal static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISiteServiceFactory, SiteServiceFactory>();
            serviceCollection.AddTransient<IBusinessRepository, BusinessRepository>();
            serviceCollection.AddTransient<ISiteRepository, SiteRepository>();
            serviceCollection.AddTransient<ISiteResourceRepository, SiteResourceRepository>();
            serviceCollection.AddTransient<ISystemServices, SystemServices>();

            //command handlers
            serviceCollection.AddTransient<ICommandHandler<CreateSite>, CreateSiteHandler>();
            serviceCollection.AddTransient<ICommandHandler<UpdateSiteDetails>, UpdateSiteDetailsHandler>();
            serviceCollection.AddTransient<ICommandHandler<UpdateSiteContact>, UpdateSiteContactHandler>();
            serviceCollection.AddTransient<ICommandHandler<CreateSiteResource>, CreateSiteResourceHandler>();
            serviceCollection.AddTransient<ICommandHandler<RemoveSiteResource>, RemoveSiteResourceHandler>();

            //event handlers
            serviceCollection.AddTransient<IEventHandler<BusinessCreated>, BusinessCreatedHandler>();

            //query handlers
            serviceCollection.AddTransient<IQueryHandler<GetSite, SiteDto>, GetSiteHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetSiteSummaries, IEnumerable<SiteSummaryDto>>, GetSiteSummariesHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetSiteResources, IEnumerable<SiteResourceDto>>, GetSiteResourcesHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetSiteAvailability, SiteAvailabilityDto>, GetSiteAvailabilityHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetSiteFireList, SiteFireListDto>, GetSiteFireListHandler>();

            return serviceCollection;
        }
    }
}
