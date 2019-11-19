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
            services.AddHttpClient<BusinessService>();          
            return services;
        }
    }
}