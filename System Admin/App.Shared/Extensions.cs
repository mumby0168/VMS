using App.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddPubSubService(this IServiceCollection services) => services.AddSingleton<IPubSubService, PubSubService>();
    }
}