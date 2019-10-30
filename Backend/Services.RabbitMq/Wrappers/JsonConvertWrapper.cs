using System;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Interfaces.Wrappers;

namespace Services.RabbitMq.Wrappers
{
    public class JsonConvertWrapper : IJsonConvertWrapper
    {
        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

        public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);
        public IServiceBusMessage DeserializeMessage(string s, Type type) => JsonConvert.DeserializeObject(s, type) as IServiceBusMessage;
    }
}
