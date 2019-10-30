using System;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Interfaces.Wrappers
{
    public interface IJsonConvertWrapper
    {
        string Serialize(object obj);

        T Deserialize<T>(string json);
        IServiceBusMessage DeserializeMessage(string s, Type type);
    }
}
