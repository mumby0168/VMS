using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Service.SignlarR
{
    public class Startup
    {
        private const string code = "secret";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<DemoHub>("/demo");
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("SignalR Demo Service");
                });
            });

            Pump(app.ApplicationServices.GetService<IHubContext<DemoHub>>());
        }

        private void Pump(IHubContext<DemoHub> hub)
        {
            Task.Run(() =>
            {
                int count = 0;
                while (true)
                {
                    count++;
                    var client = hub.Clients.Group("users:secret");
                    client?.SendAsync("pump", new
                    {
                        id = count,
                        message = "hello their"
                    });
                    Thread.Sleep(2000);
                }
            });
        }
    }
}
