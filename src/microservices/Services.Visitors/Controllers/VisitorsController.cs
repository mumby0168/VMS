using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Common.Jwt;
using Services.Common.Queries;
using Services.Visitors.Dtos;
using Services.Visitors.Queries;

namespace Services.Visitors.Controllers
{
    [Route("visitors/api")]
    public class VisitorsController : VmsControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public VisitorsController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("site/{siteId}")]
        public async Task<ActionResult<IEnumerable<VisitorDto>>> Get([FromRoute] Guid siteId)
        {
            return Collection(await _queryDispatcher.Dispatch<GetVisitorsOnSite, IEnumerable<VisitorDto>>(new GetVisitorsOnSite(siteId)));
        }

        [HttpGet("business/{businessId}")]
        public async Task<ActionResult<IEnumerable<VisitorRecordDto>>> GetVisitorsForSite([FromRoute] Guid businessId) =>
            Collection(await _queryDispatcher.Dispatch<VisitorRecordsForBusiness, IEnumerable<VisitorRecordDto>>(
                new VisitorRecordsForBusiness {BusinessId = businessId}));    
        
        [HttpGet("info/{visitorId}")]
        public async Task<ActionResult<IEnumerable<VisitorInformationDto>>> GetVisitorInformation([FromRoute] Guid visitorId) =>
            Collection(await _queryDispatcher.Dispatch<VisitorInformation, IEnumerable<VisitorInformationDto>>(
                new VisitorInformation {VisitorId = visitorId}));
    }
}