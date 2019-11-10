using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Messages.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    [Route("gateway/api/businesses/")]
    public class BusinessController : GatewayControllerBase
    {
        public BusinessController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("create")]
        [Authorize(Roles = Roles.SystemAdmin)]
        public Task<IActionResult> Create([FromBody] CreateBusiness command) 
            => Task.FromResult(PublishCommand(command));

    }
}
