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
    }
}