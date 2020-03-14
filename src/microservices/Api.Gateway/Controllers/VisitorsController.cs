using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Visitors;
using Api.Gateway.Messages.Visitors;
using Convey.HTTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    [Route("gateway/api/visitors")]
    public class VisitorsController : GatewayControllerBase
    {
        private readonly IVisitorsClient _client;

        public VisitorsController(IDispatcher dispatcher, IVisitorsClient client, HttpClientOptions options, IVmsLogger<GatewayControllerBase> logger) : base(dispatcher, options, logger)
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
            return PublishCommand(new UpdateEntryOrder( GetBusinessId(), command.EntryId, command.Order));
        }

        [Authorize(Roles = Roles.BusinessAdmin + "," + Roles.Greeting)]
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

        [Authorize(Roles = Roles.Greeting)]
        [HttpPost("create")]
        public IActionResult SubmitForm([FromBody] CreateVisitor command)
        {
            return PublishCommand(command);
        }

        
        [Authorize(Roles = Roles.Greeting)]
        [HttpGet("site/{siteId}")]
        public async Task<ActionResult<IEnumerable<VisitorDto>>> Get([FromRoute] Guid siteId)
        {
            return Collection(await _client.GetVisitorsForSiteAsync(siteId));
        }
        
        [Authorize(Roles = Roles.Greeting)]
        [HttpPost("out")]
        public IActionResult SignOut([FromBody] VisitorSignOut command)
        {
            return PublishCommand(command);
        }

        [Authorize(Roles = Roles.BusinessAdmin)]
        [Route("business/{businessId}")]
        public async Task<ActionResult<IEnumerable<VisitorRecordDto>>> GetVisitorsForBusinessAsync([FromRoute] Guid businessId) 
            => Collection(await _client.GetVisitorsForBusinessAsync(businessId));
    }
}
