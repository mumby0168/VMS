using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.Common.Queries;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Visitors.Commands;
using Services.Visitors.Dtos;
using Services.Visitors.Factories;
using Services.Visitors.Handlers.Command;
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
            services.AddTransient<IDataSpecificationRepository, DataSpecificationRepository>();
            services.AddTransient<IDataSpecificationFactory, DataSpecificationFactory>();
            services.AddTransient<IVisitorFactory, VisitorFactory>();
            services.AddTransient<IVisitorsRepository, VisitorsRepository>();
            services.AddTransient<IUserServiceClient, UserServiceClient>();
            services.AddTransient<ISiteServiceClient, SiteServiceClient>();
            services.AddTransient<IDataSpecificationValidator, DataSpecificationValidator>();
            services.AddTransient<IVisitorFormValidatorService, VisitorFormValidatorService>();


            //command handlers
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
