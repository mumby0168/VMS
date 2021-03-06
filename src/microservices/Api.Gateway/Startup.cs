using Api.Gateway.Clients;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Controllers;
using Convey;
using Convey.HTTP;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.Common.Names;
using Services.Common.Swagger;
using Services.RabbitMq.Extensions;

namespace Api.Gateway
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
            services.AddUdpLogging();
            services.AddMvcCore().AddNewtonsoftJson().AddApiExplorer();
            services.AddControllers();
            services.AddCustomAuth(_configuration);
            services.AddServiceBus();
            services.AddConvey().AddHttpClient();
            services.AddCors();
            services.AddSwagger(Services.Common.Names.Services.Gateway);

            services.AddTransient<IOperationsClient, OperationsClient>();
            services.AddTransient<IBusinessClient, BusinessClient>();
            services.AddTransient<ISiteClient, SiteClient>();
            services.AddTransient<IUsersClient, UsersClient>();
            services.AddTransient<IVisitorsClient, VisitorClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerWithUi(Services.Common.Names.Services.Gateway);

            app.UseUdpLogging(Services.Common.Names.Services.Gateway);

            app.UseCors(opts =>
            {
                opts.AllowAnyOrigin();
                opts.AllowAnyHeader();
                opts.AllowAnyMethod();
                opts.WithExposedHeaders("X-Operation");

            });
            ;
            app.UseServiceBus(Services.Common.Names.Services.Gateway, false);

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(Services.Common.Names.Services.Gateway);
                });
            });
        }
    }
}
