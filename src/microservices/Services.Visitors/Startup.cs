using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.HTTP;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common.Logging;
using Services.Common.Mongo;
using Services.Common.Names;
using Services.Common.Queries;
using Services.RabbitMq.Extensions;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Domain.Aggregate;
using Services.Visitors.Domain.Domain.Specification;
using Services.Visitors.Events.Subscribed;

namespace Services.Visitors
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddVisitorDomain()
                .AddMongoCollection<SpecificationDocument>();

            services.AddControllers();
            services.AddServiceBus()
                .AddAllHandlers();
            services.AddUdpLogging();
            services.AddQuerySupport();
            services.AddConvey().AddHttpClient();

            ServiceRegistry.RegisterServices(services);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMongo(Common.Names.Services.Visitors);

            app.UseServiceBus(Common.Names.Services.Visitors)
                .SubscribeCommand<CreateDataEntry>()
                .SubscribeCommand<UpdateEntryOrder>()
                .SubscribeCommand<DeprecateDataEntry>()
                .SubscribeCommand<CreateVisitor>()
                .SubscribeCommand<VisitorSignOut>();

            app.UseRouting();

            app.UseUdpLogging(Common.Names.Services.Visitors);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(Common.Names.Services.Visitors);
                });
            });
        }
    }
}
