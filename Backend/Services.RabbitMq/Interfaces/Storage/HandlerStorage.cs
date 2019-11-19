using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Interfaces.Storage
{
    public class HandlerStorage : IHandlerStorage
    {
        private readonly Dictionary<Type, Type> _mappings;

        public HandlerStorage()
        {
            _mappings = new Dictionary<Type, Type>();
        }

        public Type GetHandlerTypeForMessage(Type messageType) 
        {
            return _mappings[messageType];
        }

        public void RegisterHandleForType(Type messageType, Type handlerType)
        {
            _mappings.Add(messageType, handlerType);
        }
    }
}
