using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    class Dispatcher : IDispatcher
    {
        private readonly IServiceBusMessagePublisher _serviceBusMessagePublisher;
        private readonly ILogger<Dispatcher> _logger;

        public Dispatcher(IServiceBusMessagePublisher serviceBusMessagePublisher, ILogger<Dispatcher> logger)
        {
            _serviceBusMessagePublisher = serviceBusMessagePublisher;
            _logger = logger;
        }
        public Task DispatchCommand<TCommand>(TCommand message, IRequestInfo requestInfo) where TCommand : ICommand
        {
            _serviceBusMessagePublisher.PublishCommand(message, requestInfo);
            _logger.LogInformation($"{message.GetType().Name} dispatched for user: " + requestInfo.UserId);
            return Task.CompletedTask;
        }
    }
}
