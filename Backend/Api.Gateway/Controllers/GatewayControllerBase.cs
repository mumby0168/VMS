using System;
using Microsoft.AspNetCore.Mvc;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Api.Gateway.Controllers
{
    public abstract class GatewayControllerBase : ControllerBase
    {
        protected IDispatcher Dispatcher { get; }

        public GatewayControllerBase(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }
        protected IActionResult Accepted(IRequestInfo requestInfo)
        {
            //TODO: add a header for operation id for user to check.

            return base.Accepted();
        }

        protected IActionResult PublishCommand<T>(T command) where T : ICommand
        {
            var info = CreateRequestInfo();
            Dispatcher.DispatchCommand(command, info);
            return Accepted(info);
        }

        protected IRequestInfo CreateRequestInfo()
        {
            return RequestInfo.Create(Guid.NewGuid());
        }
    }
}
