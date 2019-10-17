using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System_Admin.Areas.Account.Models;
using System_Admin.Data;
using System_Admin.Services;

namespace System_Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<AuthenticationStateProvider, SystemAdminAuthenticationStateProvider>();
            services.AddSingleton<ITokenStorageService, TokenStorageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var tokenService = app.ApplicationServices.GetService<ITokenStorageService>();

            tokenService.SaveToken("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InNhbmRib3hAZG1haW4uY28udWsiLCJuYW1laWQiOiI1MmNjMDEyYy1mY2YxLTQzMjgtYWY5MS1lNGU2OTQ1MzA5MWEiLCJyb2xlIjoiU3lzdGVtQWRtaW4iLCJuYmYiOjE1NzEzMTk5NDIsImV4cCI6MTU3MTMzMDc0MiwiaWF0IjoxNTcxMzE5OTQyfQ._pIxO_7cSZOR5WGprrBuZPI0v-w3UxS-riRR0cAxah3eGjeTHG7BsnseSVhXOide0PhJGYfyMDcJKSCa_aYaIA");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
