using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Common.Queries;
using Services.Users.Dtos;
using Services.Users.Handlers.Queries;
using Services.Users.Queries;

namespace Services.Users.Controllers
{
    [Route("users/api/")]
    public class AccessRecordsController : VmsControllerBase
    {
        private readonly IQueryDispatcher _dispatcher;

        public AccessRecordsController(IQueryDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("records/{accountId}")]
        public async Task<ActionResult<IEnumerable<AccessRecordDto>>> Get([FromRoute]Guid accountId)
        {
            return Collection(await _dispatcher.Dispatch<GetPersonalAccessRecords, IEnumerable<AccessRecordDto>>(
                new GetPersonalAccessRecords(accountId, Guid.Empty)));
        }

        [HttpGet("business-records/{businessId}")]
        public async Task<ActionResult<IEnumerable<SiteAccessDetailsDto>>> GetBusinessRecords([FromRoute] Guid businessId)
        {
            return Collection(
                await _dispatcher.Dispatch<GetBusinessAccessRecords, IEnumerable<SiteAccessDetailsDto>>(
                    new GetBusinessAccessRecords(businessId)));
        }

        [HttpGet("site-availability/{siteId}")]
        public async Task<ActionResult<IEnumerable<LatestAccessRecordDto>>> GetLatestSiteRecords([FromRoute] Guid siteId)
        {
            return Collection(
                await _dispatcher.Dispatch<GetLatestSiteAccessRecords, IEnumerable<LatestAccessRecordDto>>(new GetLatestSiteAccessRecords(siteId)));
        }

        [HttpGet("on-site/{siteId}")]
        public async Task<ActionResult<IEnumerable<OnSiteAccessRecordDto>>> GetOnSite([FromRoute] Guid siteId) =>
            Collection(
                await _dispatcher.Dispatch<GetUsersOnSite, IEnumerable<OnSiteAccessRecordDto>>(
                    new GetUsersOnSite(siteId)));
    }
}