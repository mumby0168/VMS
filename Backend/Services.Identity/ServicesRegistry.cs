using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.Identity.Managers;
using Services.Identity.Repositorys;
using Services.Identity.Services;

namespace Services.Identity
{
    internal static class ServicesRegistry
    {
        public static IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            //repos
            serviceCollection.AddTransient<IIdentityRepository, IdentityRepository>();
            serviceCollection.AddTransient<IPendingIdentityRepository, PendingIdentityRepository>();
            serviceCollection.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            //services
            serviceCollection.AddTransient<IIdentityService, IdentityService>();
            serviceCollection.AddTransient<IRefreshTokenService, RefreshTokenService>();
            //managers
            serviceCollection.AddTransient<IPasswordManager, PasswordManager>();

            return serviceCollection;
        }


    }
}
