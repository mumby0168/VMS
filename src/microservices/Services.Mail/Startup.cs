using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.Common.Middleware;
using Services.Common.Names;
using Services.Mail.Config;
using Services.Mail.Messages.Events;
using Services.Mail.Messages.Handlers.Event;
using Services.Mail.Messages.Mail;
using Services.RabbitMq.Extensions;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration config)
        {
            this._configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ClientsAddresses>();
            services.AddMvcCore();
            services.AddServiceBus();
            services.AddCustomAuth(_configuration);
            services.AddUdpLogging();
            services.AddTransient<VmsExceptionMiddleware>();
            services.AddTransient<IEventHandler<PendingAdminCreated>, PendingAdminCreatedHandler>();
            services.AddTransient<IEventHandler<PendingBusinessAdminCreated>, PendingBusinessAdminCreatedHandler>();
            services.AddTransient<IEventHandler<UserAccountCreated>, UserAccountCreatedHandler>();
            services.AddSingleton<IMailManager, MailManager>();
            services.AddTransient<IEventHandler<VisitorSignedIn>, VisitorSignedInHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var clients = app.ApplicationServices.GetService<ClientsAddresses>();
            _configuration.GetSection("Clients").Bind(clients);

            app.UseRouting();
            app.UseServiceBus(Common.Names.Services.Mail)
                .SubscribeEvent<PendingAdminCreated>()
                .SubscribeEvent<PendingBusinessAdminCreated>()
                .SubscribeEvent<UserAccountCreated>()
                .SubscribeEvent<VisitorSignedIn>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
