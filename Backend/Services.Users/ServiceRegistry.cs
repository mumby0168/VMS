using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Services.Common.Queries;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Users.Commands;
using Services.Users.Domain;
using Services.Users.Events;
using Services.Users.Factories;
using Services.Users.Handlers.Command;
using Services.Users.Handlers.Events;
using Services.Users.Repositories;
using Services.Users.Services;

namespace Services.Users
{
    internal static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUsersFactory, UsersFactory>();
            serviceCollection.AddTransient<IAccountRepository, AccountRepository>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<IServicesRepository, ServicesRepository>();


            serviceCollection.AddSingleton<IAccessRecordFactory, AccessRecordFactory>();
            serviceCollection.AddTransient<IAccessRecordRepository, AccessRecordRepository>();

            serviceCollection.AddTransient<IEventHandler<UserAccountCreated>, UserAccountCreatedHandler>();

            serviceCollection.AddTransient<ICommandHandler<CreateUser>, CreateUserHandler>();
            serviceCollection.AddTransient<ICommandHandler<CreateAccessRecord>, CreateAccessRecordHandler>();

            return serviceCollection;
        }
    }
}
