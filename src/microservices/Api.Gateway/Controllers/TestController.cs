using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Gateway.Messages.Test;
using Convey.HTTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    [Route("gateway/api/test/")]
    public class TestController : GatewayControllerBase
    {
        public TestController(IDispatcher dispatcher, HttpClientOptions options, IVmsLogger<GatewayControllerBase> logger) : base(dispatcher, options, logger)
        {
            
        }




        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("pass")]
        public IActionResult Pass([FromBody]TestCommand test)
        {
            return PublishCommand(test);
        }

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("issue")]
        public IActionResult Issue([FromBody]IssueCommand test)
        {
            return PublishCommand(test);
        }
    }
}
