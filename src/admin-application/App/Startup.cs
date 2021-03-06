using Account;
using Account.Interfaces.Jwt;
using App.Businesses;
using App.Shared;
using App.Shared.Http;
using App.Shared.Operations;
using App.Sites;
using Blazor.LoadingIndicator;
using Blazored.Modal;
using Blazored.Toast;
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

            var endpoints = new Endpoints();
            Configuration.GetSection("Endpoints").Bind(endpoints);
            services.AddSingleton(typeof(Endpoints), endpoints);

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();            
            services.AddScoped<AuthenticationStateProvider, SystemAdminAuthenticationStateProvider>();     
            services.AddPubSubService();      
            services.AddBlazoredToast();
            services.AddBlazoredModal();
            services.AddBusinesses();
            services.AddAccount();
            services.AddUserContext();
            services.AddOperationsServices();
            services.AddLoadingIndicator();
            services.AddDevelopmentSettings(Configuration);
            services.AddHttpExecutor();
            services.AddCustomClients();            
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

            var ops = app.ApplicationServices.GetService<IOperationsService>();
            var manager = app.ApplicationServices.GetService<IOperationsManager>();

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
