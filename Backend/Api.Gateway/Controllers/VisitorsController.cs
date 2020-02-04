using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Visitors;
using Api.Gateway.Messages.Visitors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    [Route("gateway/api/visitors")]
    public class VisitorsController : GatewayControllerBase
    {
        private readonly IVisitorsClient _client;

        public VisitorsController(IDispatcher dispatcher, IVisitorsClient client) : base(dispatcher)
        {
            _client = client;
        }

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpPost("spec/create")]
        public IActionResult CreateDataEntry([FromBody] CreateDataEntry command) => PublishCommand(new CreateDataEntry(command.Label, command.ValidationMessage, command.ValidationCode, GetBusinessId()));

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpPost("spec/reorder")]
        public IActionResult ReorderDataEntries([FromBody] UpdateEntryOrder command)
        {
            var businessId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == CustomClaims.BusinessIdClaim);
            if (businessId is null) return Unauthorized();
            return PublishCommand(new UpdateEntryOrder(command.Entries, Guid.Parse(businessId.Value)));
        }

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpGet("specs")]
        public async Task<ActionResult<IEnumerable<DataSpecificationDto>>> GetSpecifications() => Collection(await _client.GetDataSpecificationsForBusinessAsync(GetBusinessId()));

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpGet("specs/validators")]
        public async Task<ActionResult<IEnumerable<string>>> GetSpecificationValidators() => Collection(await _client.GetDataSpecificationValidatorsAsync());

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpDelete("spec/deprecate")]
        public IActionResult DeprecateEntry([FromBody] DeprecateDataEntry command)
        {
            return PublishCommand(new DeprecateDataEntry(command.Id, GetBusinessId()));
        }

    }
}
