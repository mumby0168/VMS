using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    class Dispatcher : IDispatcher
    {
        private readonly IServiceBusMessagePublisher _serviceBusMessagePublisher;

        public Dispatcher(IServiceBusMessagePublisher serviceBusMessagePublisher)
        {
            _serviceBusMessagePublisher = serviceBusMessagePublisher;
        }
        public Task DispatchCommand<TCommand>(TCommand message, IRequestInfo requestInfo) where TCommand : ICommand
        {
            _serviceBusMessagePublisher.PublishCommand(message, requestInfo);
            return Task.CompletedTask;
        }
    }
}
