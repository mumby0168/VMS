using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Messages.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    [Route("gateway/api/users/")]
    public class UsersController : GatewayControllerBase
    {
        public UsersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [Authorize(Roles = Roles.PortalUser)]
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateUser command) => PublishCommand(command);
    }
}
