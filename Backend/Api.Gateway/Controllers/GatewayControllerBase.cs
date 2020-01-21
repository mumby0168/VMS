
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
using Services.Common.Exceptions;
using Services.Common.Jwt;
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

        protected Guid GetAccountId()
        {
            var accountId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (accountId is null) throw new VmsException("", "");
            return Guid.Parse(accountId.Value);
        }


        protected Guid GetBusinessId()
        {
            var businessId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.BusinessIdClaim);
            if (businessId is null) throw new VmsException("", "");
            return Guid.Parse(businessId.Value);
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
                case Services.Common.Names.Services.Businesses:
                    return CheckHealth("http://business:80/business/status/api/pulse", command);
                case Services.Common.Names.Services.Sites:
                    return CheckHealth("http://sites:80/sites/status/api/pulse", command);
                case Services.Common.Names.Services.Operations:
                    return CheckHealth("http://operations:80/operations/status/api/pulse", command);
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
