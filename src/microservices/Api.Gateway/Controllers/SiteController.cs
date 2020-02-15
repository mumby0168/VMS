
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Sites;
using Api.Gateway.Messages.Sites;
using Convey.HTTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.Common.Names;
using Services.RabbitMq.Interfaces.Messaging;
    
namespace Api.Gateway.Controllers
{
    [Route("gateway/api/sites/")]
    public class SiteController : GatewayControllerBase
    {
        private readonly ISiteClient _siteClient;

        public SiteController(IDispatcher dispatcher, ISiteClient siteClient) : base(dispatcher)
        {
            _siteClient = siteClient;
        }

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("create")]
        public Task<IActionResult> Create([FromBody]CreateSite command) => PublishCommand(command, Services.Common.Names.Services.Sites);

        [Authorize(Roles = Roles.SystemAdmin + "," + Roles.PortalGreeting)]
        [HttpGet("summaries/{businessId}")]
        public async Task<ActionResult<IEnumerable<SiteSummaryDto>>> GetSummaries([FromRoute] Guid businessId) => Collection(await _siteClient.GetSites(businessId));

        [Authorize(Roles = Roles.PortalUser)]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<SiteSummaryDto>>> GetSiteSummariesForBusiness()
        {
            var businessId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == CustomClaims.BusinessIdClaim);
            if (businessId is null) return Unauthorized();
            return Collection(await _siteClient.GetSites(Guid.Parse(businessId.Value)));
        }

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpGet("get/{siteId}")]
        public async Task<ActionResult<SiteDto>> Get([FromRoute] Guid siteId) => Single(await _siteClient.GetSite(siteId));

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("update")]
        public Task<IActionResult> Update([FromBody]UpdateSiteDetails command) => PublishCommand(command, Services.Common.Names.Services.Sites);

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("update-contact")]
        public Task<IActionResult> UpdateContact([FromBody]UpdateSiteContact command) => PublishCommand(command, Services.Common.Names.Services.Sites);


        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("create-site-resource")]
        public Task<IActionResult> CreateSiteResource([FromBody]CreateSiteResource command) => PublishCommand(command, Services.Common.Names.Services.Sites);

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("remove-site-resource")]
        public Task<IActionResult> RemoveSiteResource([FromBody]RemoveSiteResource command) => PublishCommand(command, Services.Common.Names.Services.Sites);

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpGet("resources/{siteId}")]
        public async Task<ActionResult<IEnumerable<SiteResourceDto>>> GetSiteResources([FromRoute] Guid siteId) => Collection(await _siteClient.GetResourcesForSite(siteId));
        
        [Authorize(Roles = Roles.PortalUser)]
        [HttpGet("availability/{siteId}")]
        public async Task<ActionResult<SiteAvailabilityDto>> GetAvailability([FromRoute] Guid siteId) => Single(await _siteClient.GetSiteAvailabilityAsync(siteId));

        [Authorize(Roles = Roles.PortalUser)]
        [HttpGet("fire/{siteId}")]
        public async Task<ActionResult<SiteFireListDto>> FireList([FromRoute] Guid siteId) => Single(await _siteClient.GetFireListForSiteAsync(siteId));

    }
}