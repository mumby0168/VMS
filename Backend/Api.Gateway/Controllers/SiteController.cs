using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Messages.Sites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    [Route("gateway/api/sites/")]
    public class SiteController : GatewayControllerBase
    {
        public SiteController(IDispatcher dispatcher) : base(dispatcher)
        {

        }

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateSite command) => PublishCommand(command);
    }
}