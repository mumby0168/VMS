using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Businesses.Domain;
using Services.Businesses.Messages.Commands;
using Services.Common.Mongo;
using Services.Common.Names;
using Services.RabbitMq.Extensions;

namespace Services.Businesses
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceBus();
            services.AddControllers();
            services.AddMongo().AddMongoCollection<Business>();
            //Adds service specific services.
            ServiceRegistry.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseMongo(ServiceNames.Businesses);
            app.UseServiceBus(ServiceNames.Businesses)
                .SubscribeCommand<CreateBusiness>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(ServiceNames.Businesses);
                });
            });
        }
    }
}