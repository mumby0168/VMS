using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Factories
{
    [ExcludeFromCodeCoverage]
    public class HandlerFactory : IHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public HandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ICommandHandler<T> ResolveCommandHandler<T>() where T : ICommand => _serviceProvider.GetService<ICommandHandler<T>>();

        public IEventHandler<T> ResolveEventHandler<T>() where T : IEvent =>
            _serviceProvider.GetService<IEventHandler<T>>();
    }
}
