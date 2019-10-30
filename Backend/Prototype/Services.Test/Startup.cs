using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common.Names;
using Services.RabbitMq.Extensions;
using Services.RabbitMq.Interfaces.Messaging;
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
            services.AddTransient<ICommandHandler<TestCommand>, TestCommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceBus(ServiceNames.Test, true)
                .SubscribeCommand<TestCommand>();

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    endpoints.MapControllers();
                    await context.Response.WriteAsync(ServiceNames.Test);
                });
            });
        }
    }
}
