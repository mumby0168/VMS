using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronicle;
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
using Services.RabbitMq.Extensions;
using Services.Users.Commands;
using Services.Users.Domain;
using Services.Users.Events;

namespace Services.Users
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceBus();
            services.AddMongo()
                .AddMongoCollection<User>()
                .AddMongoCollection<Account>();

            services.AddConvey().AddHttpClient();

            services.AddUdpLogging();

            services.AddChronicle();


            ServiceRegistry.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMongo(ServiceNames.Users);
            app.UseServiceBus(ServiceNames.Users)
                .SubscribeEvent<UserAccountCreated>()
                .SubscribeCommand<CreateUser>();

            app.UseUdpLogging(ServiceNames.Users);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(ServiceNames.Users);
                });
            });
        }
    }
}
