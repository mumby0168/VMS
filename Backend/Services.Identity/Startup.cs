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
using Services.Common.Middleware;
using Services.Common.Mongo;
using Services.Common.Names;
using Services.Identity.Domain;
using Services.Identity.Managers;
using Services.Identity.Repositorys;
using Services.Identity.Services;
using Services.RabbitMq.Extensions;

namespace Services.Identity
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddNewtonsoftJson();

            services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddServiceBus();

            services.AddCustomAuth(_configuration);

            services.AddTransient<VmsExceptionMiddleware>();

            services.AddMongo()
                .AddMongoCollection<Domain.Identity>()
                .AddMongoCollection<PendingIdentity>()
                .AddMongoCollection<RefreshToken>();


            ServicesRegistry.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseVmsExceptionHandler();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseServiceBus(ServiceNames.Identity);
            app.UseMongo(ServiceNames.Identity);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(ServiceNames.Identity);
                });
            });
        }
    }
}
