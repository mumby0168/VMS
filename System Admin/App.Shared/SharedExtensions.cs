using App.Shared.Context;
using App.Shared.Operations;
using App.Shared.Services;
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
    }
}