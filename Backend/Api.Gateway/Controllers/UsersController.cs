using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Users;
using Api.Gateway.Messages.Users.Commands;
using Api.Gateway.Messages.Users.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    [Route("gateway/api/users/")]
    public class UsersController : GatewayControllerBase
    {
        private readonly IUsersClient _client;

        public UsersController(IDispatcher dispatcher, IUsersClient client) : base(dispatcher)
        {
            _client = client;
        }

        [Authorize(Roles = Roles.PortalUser)]
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateUser command) => PublishCommand(command);

        //TODO: decide on security restriction for this.
        [AllowAnonymous]
        [HttpPost("in")]
        public IActionResult SignOn([FromBody]UserSignOn command) =>
            PublishCommand(new CreateAccessRecord(command.UserId, AccessAction.In));

        //TODO: decide on security restriction for this.
        [AllowAnonymous]
        [HttpPost("out")]
        public IActionResult SignOn([FromBody]UserSignOff command) =>
            PublishCommand(new CreateAccessRecord(command.UserId, AccessAction.Out));

        [Authorize(Roles = Roles.PortalUser)]
        [HttpGet("records")]
        public async Task<ActionResult<IEnumerable<AccessRecordDto>>> GetRecords()
        {
            var accountId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (accountId is null) return Unauthorized();
            return Ok(await _client.GetAccessRecordForUserAsync(Guid.Parse(accountId.Value)));
        }

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpGet("business-records")]
        public async Task<ActionResult<IEnumerable<SiteAccessDetailsDto>>> GetBusinessRecords()
        {
            var businessId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == CustomClaims.BusinessIdClaim);
            if (businessId is null) return Unauthorized();
            return Ok(await _client.GetBusinessAccessRecordsAsync(Guid.Parse(businessId.Value)));
        }
    }
}
