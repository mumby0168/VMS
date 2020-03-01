using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Services.Common.Logging;
using Services.Common.Mongo;
using Services.Common.Names;
using Services.RabbitMq.Extensions;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Test.Domain;
using Services.Test.Handlers;
using Services.Test.Messages.Commands;

namespace Services.Test
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddNewtonsoftJson();
            services.AddServiceBus();
            services.AddControllers();
            services.AddMongo().AddMongoCollection<Person>();
            services.AddTransient<ICommandHandler<TestCommand>, TestCommandHandler>();
            services.AddTransient<ICommandHandler<IssueCommand>, IssueCommandHandler>();
            services.AddUdpLogging();
            services.AddTransient<IPersonAggregate, PersonAggregate>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseUdpLogging(Common.Names.Services.Logs);

            app.UseServiceBus(Common.Names.Services.Test, true)
                .SubscribeCommand<TestCommand>()
                .SubscribeCommand<IssueCommand>();

            app.UseMongo(Common.Names.Services.Test);

            var repo = app.ApplicationServices.GetService<IMongoRepository<Person>>();
            var aggregate = app.ApplicationServices.GetService<IPersonAggregate>();
            


            var person = aggregate.CreatePerson("Billy", 21);
            repo.AddAsync(person);

            var personFromDb = repo.FindAsync(p => p.FirstName == "Billy").Result;
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    endpoints.MapControllers();
                    await context.Response.WriteAsync(Common.Names.Services.Test);
                });
            });

            var loggingService = app.ApplicationServices.GetService<IUdpLoggingClient>();

            loggingService.LogAsync("TEST", LogType.Info, "Hello their I am a log message", Guid.Empty, Guid.Empty);
        }
    }
}
