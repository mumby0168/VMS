using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Interfaces.Wrappers;

namespace Services.RabbitMq.Messages
{
    public class ServiceBusMessageHandler : IServiceBusMessageHandler
    {
        private readonly IJsonConvertWrapper _jsonConvertWrapper;
        private readonly IUtf8Wrapper _utf8Wrapper;
        private readonly ILogger<ServiceBusMessageHandler> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ServiceBusMessageHandler(IJsonConvertWrapper jsonConvertWrapper, IUtf8Wrapper utf8Wrapper, ILogger<ServiceBusMessageHandler> logger, IServiceProvider serviceProvider)
        {
            _jsonConvertWrapper = jsonConvertWrapper;
            _utf8Wrapper = utf8Wrapper;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task Handle<T>(object sender, BasicDeliverEventArgs args, Func<T, IRequestInfo, Task> callback) where T : IServiceBusMessage
        {
            _logger.LogInformation($"Message received key: {args.RoutingKey}");
            string json = _utf8Wrapper.GetString(args.Body);
            var split = json.Split('\t');
            var requestInfo = _jsonConvertWrapper.Deserialize<RequestInfo>(split[0]);

            var command = _jsonConvertWrapper.Deserialize<T>(split[1]);
            await callback.Invoke(command, requestInfo);

        }

        public async Task HandleUsingRoutingKey<T>(object sender, BasicDeliverEventArgs args, Func<T, IRequestInfo, Task> callback)
        {
            _logger.LogInformation($"Message received key: {args.RoutingKey}");
            string json = _utf8Wrapper.GetString(args.Body);
            var split = json.Split('\t');
            var requestInfo = _jsonConvertWrapper.Deserialize<RequestInfo>(split[0]);

            var splitKey = args.RoutingKey.Split('.').ToList();
            var ass = Assembly.GetEntryAssembly();
            string name = splitKey.Last();
            var type = ass.GetTypes().FirstOrDefault(t => t.Name == name);

            if (type == null)
            {
                _logger.LogWarning("Type could not be resolved from routing key: " + args.RoutingKey);
                throw new InvalidOperationException();
            }

            var command = _jsonConvertWrapper.DeserializeMessage(split[1], type);
            await callback.Invoke((T)command, requestInfo);
        }
    }
}
