using App.Businesses.Services;
using App.Businesses.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace App.Businesses
{
    public static class Extensions
    {
        public static IServiceCollection AddBusinesses(this IServiceCollection services)
        {
            services.AddScoped<CreateBusinessViewModel>();
            services.AddScoped<UpdateContactDetailsViewModel>();
            services.AddScoped<UpdateBusinessOfficeViewModel>();
            services.AddScoped<UpdateBusinessDetailsViewModel>();
            services.AddScoped<CreateBusinessAdminViewModel>();
            services.AddTransient<IAdminAccountService, AdminAccountService>();
            services.AddHttpClient<BusinessService>();          
            return services;
        }
    }
}