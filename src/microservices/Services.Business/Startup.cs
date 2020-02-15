using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Business.Messages.Commands;
using Services.Common.Generation;
using Services.Common.Logging;
using Services.Common.Mongo;
using Services.Common.Names;
using Services.Common.Queries;
using Services.RabbitMq.Extensions;

namespace Services.Business
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUdpLogging();
            services.AddServiceBus();
            services.AddControllers();
            services.AddMongo().AddMongoCollection<Domain.Business>();
            services.AddQuerySupport();
            services.AddNumberGenerator();
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



            app.UseUdpLogging(Common.Names.Services.Businesses);
            app.UseMongo(Common.Names.Services.Businesses);
            app.UseServiceBus(Common.Names.Services.Businesses)
                .SubscribeCommand<CreateBusiness>()
                .SubscribeCommand<UpdateBusinessDetails>()
                .SubscribeCommand<UpdateHeadContact>()
                .SubscribeCommand<UpdateHeadOffice>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(Common.Names.Services.Businesses);
                });
            });
        }
    }
}
    