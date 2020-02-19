using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Common.Generation;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.Common.Middleware;
using Services.Common.Mongo;
using Services.Common.Queries;
using Services.Common.Swagger;
using Services.Identity.Domain;
using Services.Identity.Managers;
using Services.Identity.Messages.Events;
using Services.Identity.Messages.Events.Subscribed;
using Services.Identity.Repositorys;
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

            services.AddNumberGenerator();

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


            var publisher = app.ApplicationServices.GetService<IServiceBusMessagePublisher>();
            CheckSeed(app.ApplicationServices.GetService<IIdentityRepository>(), app.ApplicationServices.GetService<IPasswordManager>(), publisher);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(Common.Names.Services.Identity);
                });
            });
        }
            
        private void CheckSeed(IIdentityRepository repo, IPasswordManager passwordManager, IServiceBusMessagePublisher publisher)
        {
            //if (repo.GetByEmailAndRole("johnadmin@test.com", Roles.BusinessAdmin).Result == null)
            //{
            //    var password = passwordManager.EncryptPassword("Test123");
            //    repo.AddAsync(new Domain.Identity("johnadmin@test.com", password.Hash, password.Salt, Roles.BusinessAdmin));
            //}

            //if (repo.GetByEmailAndRole("b-standard@test.com", Roles.StandardPortalUser).Result == null)
            //{
            //    var password = passwordManager.EncryptPassword("Test123");
            //    repo.AddAsync(new Domain.Identity("b-standard@test.com", password.Hash, password.Salt, Roles.StandardPortalUser));

            //}



            if (repo.GetByEmailAndRole("test@test.com", Roles.SystemAdmin).Result == null)
            {
                var password = passwordManager.EncryptPassword("Test123");
                repo.AddAsync(new Domain.Identity("test@test.com", password.Hash, password.Salt, Roles.SystemAdmin));
            }           
        }
    }
}
