using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Common.Queries;
using Services.Sites.Dtos;
using Services.Sites.Messages.Queries;

namespace Services.Sites.Controllers
{
    [Route("site/api/")]
    public class SiteController : VmsControllerBase
    {
        private readonly IQueryDispatcher _dispatcher;

        public SiteController(IQueryDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("summaries/{businessId}")]
        public async Task<ActionResult<IEnumerable<SiteSummaryDto>>> Summaries([FromRoute] Guid businessId) => Collection(await _dispatcher.Dispatch<GetSiteSummaries, IEnumerable<SiteSummaryDto>>(new GetSiteSummaries(businessId)));

        [HttpGet("{siteId}")]
        public async Task<ActionResult<SiteDto>> Get([FromRoute] Guid siteId) => Single(await _dispatcher.Dispatch<GetSite, SiteDto>(new GetSite(siteId)));

        [HttpGet("resources/{siteId}")]
        public async Task<ActionResult<IEnumerable<SiteResourceDto>>> GetResources([FromRoute] Guid siteId) => Single(await _dispatcher.Dispatch<GetSiteResources, IEnumerable<SiteResourceDto>>(new GetSiteResources(siteId)));

        [HttpGet("availability/{siteId}")]
        public async Task<ActionResult<SiteAvailabilityDto>> GetAvailability([FromRoute] Guid siteId) => Single(await _dispatcher.Dispatch<GetSiteAvailability, SiteAvailabilityDto>(new GetSiteAvailability(siteId)));

        [HttpGet("fire/{siteId}")]
        public async Task<ActionResult<SiteFireListDto>> GetFireList([FromRoute] Guid siteId) =>
            Single(await _dispatcher.Dispatch<GetSiteFireList, SiteFireListDto>(new GetSiteFireList(siteId)));
    }
}
