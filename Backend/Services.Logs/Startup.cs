using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common.Mongo;
using Services.Common.Names;
using Services.Logs.Decode;
using Services.Logs.Domain;
using Services.Logs.Factories;
using Services.Logs.Repositorys;
using Services.Logs.Server;

namespace Services.Logs
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ILogFactory, LogFactory>();
            services.AddMongo().AddMongoCollection<Log>();
            services.AddCors();
            services.AddSingleton<IUdpServer, UdpServer>();
            services.AddTransient<ILogsRepository, LogRepository>();
            services.AddTransient<ILogDecoder, LogDecoder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(opts =>
            {
                opts.AllowAnyHeader();
                opts.AllowAnyMethod();
                opts.AllowAnyOrigin();
            });

            app.UseMongo(ServiceNames.Logs);

            var udpServer = app.ApplicationServices.GetService<IUdpServer>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(ServiceNames.Logs);
                });
            });

            lifetime.ApplicationStopping.Register(udpServer.Stop);

            udpServer.Begin(11000);
        }
    }
}
