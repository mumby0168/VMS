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

        [HttpGet("records/{userId}")]
        public async Task<ActionResult<IEnumerable<AccessRecordDto>>> Get([FromRoute]Guid userId)
        {
            return Collection(await _dispatcher.Dispatch<GetPersonalAccessRecords, IEnumerable<AccessRecordDto>>(
                new GetPersonalAccessRecords(userId)));
        }

        [HttpGet("business-records/{businessId}")]
        public async Task<ActionResult<IEnumerable<SiteAccessDetailsDto>>> GetBusinessRecords([FromRoute] Guid businessId)
        {
            return Collection(
                await _dispatcher.Dispatch<GetBusinessAccessRecords, IEnumerable<SiteAccessDetailsDto>>(
                    new GetBusinessAccessRecords(businessId)));
        }
    }
}