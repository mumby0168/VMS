﻿using System;
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

namespace Services.Visitors
{
    public static class ServiceRegistry
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IDataSpecificationRepository, DataSpecificationRepository>();
            services.AddTransient<IDataSpecificationFactory, DataSpecificationFactory>();


            //command handlers
            services.AddTransient<ICommandHandler<CreateDataEntry>, CreateDataEntryHandler>();
            services.AddTransient<ICommandHandler<UpdateEntryOrder>, UpdateEntryOrderHandler>();

            //query handlers 
            services
                .AddTransient<IQueryHandler<GetBusinessDataSpecifications, IEnumerable<DataSpecificationDto>>,
                    GetBusinessDataSpecificationHandler>();
        }
    }
}
