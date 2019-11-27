using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common.Mongo;
using Services.Common.Names;
using Services.Common.Queries;
using Services.RabbitMq.Extensions;
using Services.Sites.Domain;
using Services.Sites.Handlers.Command;
using Services.Sites.Messages.Commands;
using Services.Sites.Messages.Events;

namespace Services.Sites
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceBus();
            services.AddMongo()
                .AddMongoCollection<Site>()
                .AddMongoCollection<Business>()
                .AddMongoCollection<SiteResource>();

            services.AddQuerySupport();

            services.AddControllers();

            ServiceRegistry.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMongo(ServiceNames.Sites);

            app.UseServiceBus(ServiceNames.Sites)
                .SubscribeCommand<CreateSite>()
                .SubscribeCommand<UpdateSiteDetails>()
                .SubscribeCommand<RemoveSiteResource>()
                .SubscribeCommand<CreateSiteResource>()
                .SubscribeCommand<UpdateSiteContact>()
                .SubscribeEvent<BusinessCreated>();


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(ServiceNames.Sites);
                });
            });
        }
    }
}
