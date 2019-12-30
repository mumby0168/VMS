using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Services.Common.Queries;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Users.Commands;
using Services.Users.Domain;
using Services.Users.Dtos;
using Services.Users.Events;
using Services.Users.Factories;
using Services.Users.Handlers.Command;
using Services.Users.Handlers.Events;
using Services.Users.Handlers.Queries;
using Services.Users.Queries;
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
            serviceCollection.AddTransient<ISiteRepository, SiteRepository>();


            serviceCollection.AddSingleton<IAccessRecordFactory, AccessRecordFactory>();
            serviceCollection.AddTransient<IAccessRecordRepository, AccessRecordRepository>();
            serviceCollection.AddTransient<ISiteFactory, SiteFactory>();

            serviceCollection.AddTransient<IEventHandler<UserAccountCreated>, UserAccountCreatedHandler>();

            serviceCollection
                .AddTransient<IQueryHandler<GetPersonalAccessRecords, IEnumerable<AccessRecordDto>>,
                    GetPersonalAccessRecordsHandler>();
            serviceCollection
                .AddTransient<IQueryHandler<GetBusinessAccessRecords, IEnumerable<SiteAccessDetailsDto>>,
                    GetBusinessAccessRecordsHandler>();

            serviceCollection.AddTransient<ICommandHandler<CreateUser>, CreateUserHandler>();
            serviceCollection.AddTransient<ICommandHandler<CreateAccessRecord>, CreateAccessRecordHandler>();

            serviceCollection.AddTransient<IEventHandler<SiteCreated>, SiteCreatedHandler>();

            return serviceCollection;
        }
    }
}
