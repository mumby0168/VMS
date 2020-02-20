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

        [Authorize(Roles = Roles.PortalGreeting)]
        [HttpPost("in")]
        public IActionResult SignOn([FromBody]UserSignOn command) =>
            PublishCommand(new CreateAccessRecord(command.Code, AccessAction.In, command.SiteId));

        [Authorize(Roles = Roles.PortalGreeting)]
        [HttpPost("out")]
        public IActionResult SignOn([FromBody]UserSignOff command) =>
            PublishCommand(new CreateAccessRecord(command.Code, AccessAction.Out, command.SiteId));

        [Authorize(Roles = Roles.PortalUser)]
        [HttpGet("records")]
        public async Task<ActionResult<IEnumerable<AccessRecordDto>>> GetRecords()
        {
            return Collection(await _client.GetAccessRecordForUserAsync(GetAccountId()));
        }

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpGet("records/{userId}")]
        public async Task<ActionResult<IEnumerable<AccessRecordDto>>> GetRecordForUser(Guid userId)
        {
            return Collection(await _client.GetAccessRecordForUserAsyncById(userId));
        }
        

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpGet("business-records")]
        public async Task<ActionResult<IEnumerable<SiteAccessDetailsDto>>> GetBusinessRecords()
        {
            return Collection(await _client.GetBusinessAccessRecordsAsync(GetBusinessId()));
        }

        [Authorize(Roles = Roles.PortalUser)]
        [HttpGet("info")]
        public async Task<ActionResult<UserInfoDto>> GetUserInfo()
        {
            return Single(await _client.GetUserInfo(GetAccountId()));
        }


        //TODO: this could be better placed in the site API Controller
        [Authorize(Roles = Roles.Greeting)]
        [HttpGet("site-state/{siteId}")]
        public async Task<ActionResult<IEnumerable<LatestAccessRecordDto>>> GetLatestAccessRecordsForSiteAsync([FromRoute] Guid siteId) 
        {
            return Collection(await _client.GetLatestStateForSite(siteId));
        }


        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserSnapshotDto>>> GetUserSnapshots() 
            => Collection(await _client.GetUsersForBusiness(GetBusinessId()));
    }
}
