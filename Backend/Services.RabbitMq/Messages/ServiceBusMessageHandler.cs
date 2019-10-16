using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Interfaces.Wrappers;

namespace Services.RabbitMq.Messages
{
    public class ServiceBusMessageHandler : IServiceBusMessageHandler
    {
        private readonly IJsonConvertWrapper _jsonConvertWrapper;
        private readonly IUtf8Wrapper _utf8Wrapper;

        public ServiceBusMessageHandler(IJsonConvertWrapper jsonConvertWrapper, IUtf8Wrapper utf8Wrapper)
        {
            _jsonConvertWrapper = jsonConvertWrapper;
            _utf8Wrapper = utf8Wrapper;
        }

        public async Task Handle<T>(object sender, BasicDeliverEventArgs args, Func<ServiceBusMessageBase<T>, Task> callback) where T : IServiceBusMessage
        {
            string json = _utf8Wrapper.GetString(args.Body);
            var messageBase = _jsonConvertWrapper.Deserialize<ServiceBusMessageBase<T>>(json);
            await callback.Invoke(messageBase);
        }
    }
}
