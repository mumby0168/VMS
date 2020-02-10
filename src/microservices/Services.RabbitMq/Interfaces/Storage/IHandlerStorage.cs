using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Interfaces.Storage
{
    public interface IHandlerStorage
    {
        Type GetHandlerTypeForMessage(Type messagetType);

        void RegisterHandleForType(Type messageType, Type handlerType);
    }
}
