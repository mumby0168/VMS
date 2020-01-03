using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Services.Visitors.Commands;
using Services.Visitors.Domain;

namespace Services.Visitors
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMongo()
                .AddMongoCollection<DataSpecification>()
                .AddMongoCollection<Visitor>();

            services.AddControllers();
            services.AddServiceBus();
            services.AddUdpLogging();
            services.AddQuerySupport();

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
                .SubscribeCommand<UpdateEntryOrder>();

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
