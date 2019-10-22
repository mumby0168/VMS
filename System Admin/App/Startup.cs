using Account;
using Account.Interfaces.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Manager
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
            services.AddHttpClient();
            services.AddScoped<AuthenticationStateProvider, SystemAdminAuthenticationStateProvider>();           

            services.AddAccount();
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

            // var accountClient = app.ApplicationServices.GetService<IAccountClient>();

            // await accountClient.SignIn("sandbox@dmain.co.uk", "Pa$$word123");

            var tokenService = app.ApplicationServices.GetService<ITokenStorageService>();

            tokenService.SaveToken("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InNhbmRib3hAZG1haW4uY28udWsiLCJuYW1laWQiOiI1MmNjMDEyYy1mY2YxLTQzMjgtYWY5MS1lNGU2OTQ1MzA5MWEiLCJyb2xlIjoiU3lzdGVtQWRtaW4iLCJuYmYiOjE1NzE3NTM2ODgsImV4cCI6MTU3MTc2NDQ4OCwiaWF0IjoxNTcxNzUzNjg4fQ.Bg0lmbc6mZw1c--fxkqAfORkXVYVx1xj874LFXO69c5ydfvlKyxOPG-Jt8Hzzgi5M0ST8P50LLSj-SvjS4oqjA");

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
