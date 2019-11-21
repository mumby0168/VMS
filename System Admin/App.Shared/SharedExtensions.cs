using App.Shared.Context;
using App.Shared.Operations;
using App.Shared.Services;
using App.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Shared
{
    public static class SharedExtensions
    {
        public static IServiceCollection AddPubSubService(this IServiceCollection services) => services.AddSingleton<IPubSubService, PubSubService>();

        public static IServiceCollection AddUserContext(this IServiceCollection services) => services.AddSingleton<IUserContext, UserContext>();

        public static IServiceCollection AddOperationsServices(this IServiceCollection services)
        {
            services.AddSingleton<IOperationsService, OperationsService>();
            services.AddSingleton<IOperationsManager, OperationsManager>();
            services.AddHttpClient<IOperationsClient, OperationsClient>();
            return services;
        }

        public static IServiceCollection AddCustomClients(this IServiceCollection services) 
        {            
            services.AddSingleton<IHttpClient, CustomHttpClient>();
            return services;
        }



        public static IServiceCollection AddHttpExecutor(this IServiceCollection services) => services.AddTransient<IHttpExecutor, HttpExecutor>();
        
        public static IServiceCollection AddDevelopmentSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new DevelopmentSettings();
            configuration.GetSection(typeof(DevelopmentSettings).Name).Bind(settings);
            services.AddSingleton<DevelopmentSettings>(settings);
            return services;
        }
    }
}