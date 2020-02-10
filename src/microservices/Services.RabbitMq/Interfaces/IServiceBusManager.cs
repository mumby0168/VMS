using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
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

        IServiceBusManager SubscribeAllMessages<T>(Assembly currentAssembly, IEnumerable<Type> excludedTypes = null) where T : IGenericBusHandler;
    }
}   
