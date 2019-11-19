using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Business;
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
        private readonly IBusinessClient _client;

        public BusinessController(IDispatcher dispatcher, IBusinessClient client) : base(dispatcher)
        {
            _client = client;
        }

        [HttpPost("create")]
        [Authorize(Roles = Roles.SystemAdmin)]
        public Task<IActionResult> Create([FromBody] CreateBusiness command) 
            => Task.FromResult(PublishCommand(command));

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessSummaryDto>>> Get() => Collection(await _client.GetBusinessSummaries());

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id) => Single(await _client.GetBusiness(id));

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("update-details")]
        public Task<IActionResult> UpdateDetails([FromBody] UpdateBusinessDetails command) =>
            Task.FromResult(PublishCommand(command));

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("update-contact")]
        public Task<IActionResult> UpdateContact([FromBody] UpdateHeadContact command) =>
            Task.FromResult(PublishCommand(command));

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("update-office")]
        public Task<IActionResult> UpdateOffice([FromBody] UpdateHeadOffice command) =>
            Task.FromResult(PublishCommand(command));

    }
}
