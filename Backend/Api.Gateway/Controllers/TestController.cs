using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Messages.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    [Route("api/test/")]
    public class TestController : GatewayControllerBase
    {
        public TestController(IDispatcher dispatcher) : base(dispatcher)
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
