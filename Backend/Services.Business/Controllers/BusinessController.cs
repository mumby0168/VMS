using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Business.Dtos;
using Services.Business.Messages.Commands;
using Services.Business.Messages.Queries;
using Services.Common.Base;
using Services.Common.Queries;

namespace Services.Business.Controllers
{
    [Route("business/api/")]
    public class BusinessController : VmsControllerBase
    {
        private readonly IQueryDispatcher _dispatcher;

        public BusinessController(IQueryDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public Task<IEnumerable<BusinessSummaryDto>> Get()
        {
            return _dispatcher.Dispatch<BusinessesSummary, IEnumerable<BusinessSummaryDto>>(new BusinessesSummary());
        }

        [HttpGet("{id}")]
        public Task<BusinessDto> Get([FromRoute] Guid id)
        {
            return _dispatcher.Dispatch<GetBusiness, BusinessDto>(new GetBusiness() {Id = id});
        }
    }
}
