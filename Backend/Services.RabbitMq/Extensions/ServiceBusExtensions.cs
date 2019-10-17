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
            return services;
        }

        public static IServiceCollection AddAllHandlers(this IServiceCollection services)
        {
            var assembly = Assembly.GetEntryAssembly();
            var types = assembly.GetTypes();

            var handlers = new List<Type>();

            foreach (var type in types)
            {
                if(type.Namespace.Contains("Handlers")) handlers.Add(type);
            }

            foreach (var handler in handlers)
            {
                if (handler.Namespace.Contains("Command"))
                {
                    var commandHandler = typeof(ICommandHandler<>);
                    var implements = handler.GetInterfaces().First();
                    var arguments = implements.GetGenericArguments();
                    var def = commandHandler.MakeGenericType(arguments);
                    services.AddTransient(def, handler);

                }
                else if (handler.Namespace.Contains("Event"))
                {
                    var commandHandler = typeof(IEventHandler<>);
                    var arguments = handler.GetGenericArguments();
                    var def = commandHandler.MakeGenericType(arguments);
                    services.AddTransient(def, handler);
                }
            }
            return services;
        }

        public static IServiceBusManager UseServiceBus(this IApplicationBuilder app, string serviceName)
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
