using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.Common.Middleware;
using Services.Common.Mongo;
using Services.Common.Names;
using Services.Common.Queries;
using Services.Common.Swagger;
using Services.Identity.Domain;
using Services.Identity.Managers;
using Services.Identity.Messages.Events;
using Services.Identity.Messages.Events.Subscribed;
using Services.Identity.Repositorys;
using Services.Identity.Services;
using Services.RabbitMq.Extensions;
using Services.RabbitMq.Messages;

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

            services.AddUdpLogging();
            services.AddMvcCore()
                .AddNewtonsoftJson()
                .AddApiExplorer();

            services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddServiceBus();

            services.AddCustomAuth(_configuration);

            services.AddTransient<VmsExceptionMiddleware>();
            services.AddQuerySupport();

            services.AddMongo()
                .AddMongoCollection<Domain.Identity>()
                .AddMongoCollection<PendingIdentity>()
                .AddMongoCollection<RefreshToken>()
                .AddMongoCollection<Business>()
                .AddMongoCollection<ResetRequest>();

            services.AddCors();

            services.AddSwagger(Common.Names.Services.Identity);



            ServicesRegistry.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseVmsExceptionHandler();

            app.UseUdpLogging(Common.Names.Services.Identity);
            app.UseCors(opts =>
            {
                opts.AllowAnyHeader();
                opts.AllowAnyMethod();
                opts.AllowAnyOrigin();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMongo(Common.Names.Services.Identity);

            app.UseSwaggerWithUi(Common.Names.Services.Identity);

            app.UseServiceBus(Common.Names.Services.Identity)
                .SubscribeEvent<BusinessCreated>();
            

            CheckSeed(app.ApplicationServices.GetService<IIdentityRepository>(), app.ApplicationServices.GetService<IPasswordManager>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(Common.Names.Services.Identity);
                });
            });
            
            app.ApplicationServices.GetService<IServiceBusMessagePublisher>().PublishEvent(new TestEvent("Hello Billy", "code_1", "This is a test"), RequestInfo.Empty);
        }
        
        private void CheckSeed(IIdentityRepository repo, IPasswordManager passwordManager)
        {
            if(repo.GetByEmailAndRole("test@test.com", Roles.SystemAdmin).Result == null)
            {
                var password = passwordManager.EncryptPassword("Test123");
                repo.AddAsync(new Domain.Identity("test@test.com", password.Hash, password.Salt, Roles.SystemAdmin));
            }

            if (repo.GetStandardAccountsForBusinessAsync(Guid.Parse("4e60143d-2a49-4f6c-a069-2b84deb67641")).Result.Count() == 0)
            {
                for(int i = 0; i < 6; i++)
                {
                    var password = passwordManager.EncryptPassword("Test123");
                    var account = new Domain.Identity($"test@test{i}.com", password.Hash, password.Salt, Roles.StandardPortalUser, Guid.Parse("4e60143d-2a49-4f6c-a069-2b84deb67641"));
                    repo.AddAsync(account);
                }
            }
        }
    }
}
