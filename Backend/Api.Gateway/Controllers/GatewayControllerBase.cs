
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Convey.HTTP;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Common.Names;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Api.Gateway.Controllers
{
    public abstract class GatewayControllerBase : VmsControllerBase
    {
        private readonly HttpClient _client;
        private const string OperationHeader = "X-Operation";   
        protected IDispatcher Dispatcher { get; }

        protected GatewayControllerBase(IDispatcher dispatcher)
        {
            _client = new HttpClient();
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

        private async Task<IActionResult> CheckHealth<T>(string healthEndpoint, T command) where T : ICommand
        {
            try
            {
                _client.Timeout = TimeSpan.FromSeconds(1);
                var res = await _client.GetAsync(healthEndpoint);
                if (res.StatusCode == HttpStatusCode.OK)
                    return PublishCommand(command);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }

        protected Task<IActionResult> PublishCommand<T>(T command, string service) where T : ICommand
        {
            switch (service)
            {
                case ServiceNames.Businesses:
                    return CheckHealth("http://localhost:5013/business/status/api/pulse", command);
                case ServiceNames.Sites:
                    return CheckHealth("http://localhost:5014/sites/status/api/pulse", command);
                case ServiceNames.Operations:
                    return CheckHealth("http://localhost:5012/operations/status/api/pulse", command);
                default:
                    throw new NotImplementedException();
            }
        }

        protected IRequestInfo CreateRequestInfo()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return RequestInfo.Create(Guid.Parse(id));
        }
    }
}
