using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Services.RabbitMq.Exchange;
using Services.RabbitMq.Factories;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Connection;
using Services.RabbitMq.Interfaces.Exchange;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Interfaces.Queues;
using Services.RabbitMq.Interfaces.Settings;
using Services.RabbitMq.Interfaces.Wrappers;
using Services.RabbitMq.Managers;
using Services.RabbitMq.Messages;
using Services.RabbitMq.Queue;
using Services.RabbitMq.Settings;
using Services.RabbitMq.Wrappers;

namespace Services.RabbitMq.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceBusExtensions
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services)
        {
            services.AddSingleton<IServiceBusConnection, ServiceBusConnection>();
            services.AddSingleton<IServiceSettings, ServiceSettings>();
            services.AddSingleton<IServiceBusManager, ServiceBusManager>();
            services.AddTransient<IJsonConvertWrapper, JsonConvertWrapper>();
            services.AddTransient<IUtf8Wrapper, Utf8Wrapper>();
            services.AddTransient<IBase64Wrapper, Base64Wrapper>();
            services.AddTransient<IServiceBusQueueFactory, ServiceBusQueueFactory>();
            services.AddTransient<IServiceBusExchangeFactory, ServiceBusExchangeFactory>();
            services.AddTransient<IServiceBusQueue, ServiceBusQueue>();
            services.AddTransient<IServiceBusExchange, ServiceBusExchange>();
            services.AddTransient<IServiceBusMessageHandler, ServiceBusMessageHandler>();
            services.AddTransient<IServiceBusMessageSubscriber, ServiceBusMessageSubscriber>();
            services.AddTransient<IServiceBusMessagePublisher, ServiceBusMessagePublisher>();
            services.AddTransient<IServiceBusConnectionFactory, ServiceBusConnectionFactory>();
            services.AddTransient<IHandlerFactory, HandlerFactory>();
            services.AddTransient<IServiceBusConsumerFactory, ServiceBusConsumerFactory>();
            services.AddTransient<IDispatcher, Dispatcher>();
            return services;
        }

        public static IServiceCollection AddAllHandlers(this IServiceCollection services)
        {
            services.Scan(selector =>
                selector.FromAssemblies(Assembly.GetEntryAssembly())
                
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))).AsImplementedInterfaces().WithTransientLifetime()
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))).AsImplementedInterfaces().WithTransientLifetime()
                    );

            return services;
        }

        public static IServiceCollection RegisterGenericMessageHandler<T>(this IServiceCollection serviceCollection)
            where T : IGenericBusHandler
        {
            serviceCollection.AddTransient(typeof(IGenericBusHandler), typeof(T));
            return serviceCollection;
        }

        public static IServiceBusManager UseServiceBus(this IApplicationBuilder app, string serviceName, bool declareQueue = true)
        {
            var manager = (IServiceBusManager)app.ApplicationServices.GetService(typeof(IServiceBusManager));
            manager.CreateConnection(
                new ServiceBusSettings()
                {
                    HostName = "localhost"
                },
                new ServiceSettings()
                {
                    Name = serviceName
                });

            return manager;
        }
    }
}
