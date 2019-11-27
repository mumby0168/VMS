using App.Sites.Services;
using App.Sites.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Sites
{
    public static class Extensions
    {
        public static IServiceCollection AddSites(this IServiceCollection services)
        {
            services.AddScoped<CreateSiteViewModel>();
            services.AddScoped<AddressFormViewModel>();
            services.AddScoped<ContactFormViewModel>();
            services.AddHttpClient<SiteService>();
            return services;
        }
    }
}
