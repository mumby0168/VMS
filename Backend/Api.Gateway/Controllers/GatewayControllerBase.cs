using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Api.Gateway.Controllers
{
    public abstract class GatewayControllerBase : VmsControllerBase
    {
        private const string OperationHeader = "X-Operation";   
        protected IDispatcher Dispatcher { get; }

        protected GatewayControllerBase(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }
        protected IActionResult Accepted(IRequestInfo requestInfo)
        {
            HttpContext.Response.Headers.Add(OperationHeader, requestInfo.OperationId.ToString());
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
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return RequestInfo.Create(Guid.Parse(id));
        }
    }
}
