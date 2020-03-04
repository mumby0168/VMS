using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Services.Common.Queries;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Visitors.Commands;
using Services.Visitors.Dtos;
using Services.Visitors.Events.Subscribed;
using Services.Visitors.Handlers.Command;
using Services.Visitors.Handlers.Events;
using Services.Visitors.Handlers.Queries;
using Services.Visitors.Queries;
using Services.Visitors.Repositorys;
using Services.Visitors.Services;

namespace Services.Visitors
{
    public static class ServiceRegistry
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ISpecificationRepository, SpecificationRepository>();
            services.AddTransient<IVisitorsRepository, VisitorsRepository>();
            services.AddTransient<IUserServiceClient, UserServiceClient>();
            services.AddTransient<ISiteServiceClient, SiteServiceClient>();
            services.AddTransient<IDataSpecificationValidator, DataSpecificationValidator>();
            services.AddTransient<IVisitorFormValidatorService, VisitorFormValidatorService>();

            services.AddTransient<IEventHandler<BusinessCreated>, BusinessCreatedHandler>();


            //command handlers
            services.AddTransient<ICommandHandler<CreateVisitor>, CreateVisitorHandler>();
            services.AddTransient<ICommandHandler<CreateDataEntry>, CreateDataEntryHandler>();
            services.AddTransient<ICommandHandler<UpdateEntryOrder>, UpdateEntryOrderHandler>();
            services.AddTransient<ICommandHandler<DeprecateDataEntry>, DeprecateDataEntryHandler>();

            //query handlers 
            services
                .AddTransient<IQueryHandler<GetBusinessDataSpecifications, IEnumerable<DataSpecificationDto>>,
                    GetBusinessDataSpecificationHandler>();
        }
    }
}
