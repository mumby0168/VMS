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

        public async Task Handle<T>(object sender, BasicDeliverEventArgs args, Func<T, IRequestInfo, Task> callback) where T : IServiceBusMessage
        {
            string json = _utf8Wrapper.GetString(args.Body);
            var split = json.Split('.');
            var requestInfo = _jsonConvertWrapper.Deserialize<IRequestInfo>(split[0]);
            var command = _jsonConvertWrapper.Deserialize<T>(split[1]);
            await callback.Invoke(command, requestInfo);
        }
    }
}
