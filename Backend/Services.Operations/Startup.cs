using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Convey;
using Convey.Persistence.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common.Logging;
using Services.Common.Names;
using Services.Operations.Handlers;
using Services.Operations.Messages.Events.Push;
using Services.Operations.Services;
using Services.RabbitMq.Extensions;

namespace Services.Operations
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddUdpLogging();
            services.AddServiceBus()
                .RegisterGenericMessageHandler<GenericBusHandler>();

            services.AddTransient<GenericBusHandler>();
            services
                .AddConvey()
                .AddRedis();

            services.AddControllers();

            services.AddTransient<IOperationsCache, OperationsCache>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseUdpLogging(Common.Names.Services.Operations);

            app.UseServiceBus(Common.Names.Services.Operations)
                .SubscribeAllMessages<GenericBusHandler>(Assembly.GetExecutingAssembly()
                    ,new [] { typeof(OperationComplete), typeof(OperationFailed), typeof(OperationPending) });

            app.UseRouting();
                
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(Common.Names.Services.Operations);
                });
            });
        }
    }
}
