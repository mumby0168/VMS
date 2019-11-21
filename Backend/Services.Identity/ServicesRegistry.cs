using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.Identity.Handlers;
using Services.Identity.Managers;
using Services.Identity.Messages.Events.Subscribed;
using Services.Identity.Repositorys;
using Services.Identity.Services;
using Services.RabbitMq.Interfaces.Messaging;

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
            serviceCollection.AddTransient<IBusinessRepository, BusinessRepository>();
            //services
            serviceCollection.AddTransient<IAdminIdentityService, AdminIdentityService>();
            serviceCollection.AddTransient<IRefreshTokenService, RefreshTokenService>();
            //managers
            serviceCollection.AddTransient<IPasswordManager, PasswordManager>();
            //handler
            serviceCollection.AddTransient<IEventHandler<BusinessCreated>, BusinessCreatedHandler>();

            return serviceCollection;
        }


    }
}
