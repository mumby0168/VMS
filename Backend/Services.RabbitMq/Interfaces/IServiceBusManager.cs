using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Interfaces.Settings;

namespace Services.RabbitMq.Interfaces
{
    public interface IServiceBusManager
    {
        void CreateConnection(IServiceBusSettings serviceBusSettings, IServiceSettings serviceSettings,
            bool declareQueue = true);

        void CloseConnection();

        IServiceBusManager SubscribeCommand<T>(string serviceNamespace = null) where T : ICommand;

        IServiceBusManager SubscribeEvent<T>(string serviceNamespace = null) where T : IEvent;

        IServiceBusManager SubscribeAllEvents<T>(Assembly currentAssembly) where T : IGenericEventHandler;
        IServiceBusManager SubscribeAllCommands<T>(Assembly currentAssembly);
    }
}   
