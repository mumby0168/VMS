using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Convey;
using Convey.HTTP;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.Common.Names;
using Services.Push.Clients;
using Services.Push.Handlers;
using Services.Push.Hubs;
using Services.Push.Services;
using Services.RabbitMq.Extensions;

namespace Services.Push
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddServiceBus();
            services.AddConvey().AddHttpClient();
            services.RegisterGenericMessageHandler<PushServiceHandler>();
            services.AddSingleton<ITokensClient, TokensClient>();
            services.AddUdpLogging();
            services.AddSingleton<IPushService, PushService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseUdpLogging(ServiceNames.Push);

            app.UseRouting();

            app.UseServiceBus(ServiceNames.Push).SubscribeAllMessages<PushServiceHandler>(Assembly.GetExecutingAssembly());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<VmsHub>("/operations");
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(ServiceNames.Push);
                });
            });
        }
    }
}
