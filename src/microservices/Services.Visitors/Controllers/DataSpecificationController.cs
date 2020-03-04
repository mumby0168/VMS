using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Common.Queries;
using Services.Visitors.Domain;
using Services.Visitors.Dtos;
using Services.Visitors.Queries;

namespace Services.Visitors.Controllers
{
    [Route("visitors/api/")]
    public class DataSpecificationController : VmsControllerBase
    {
        private readonly IQueryDispatcher _dispatcher;

        public DataSpecificationController(IQueryDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("spec/validation-options")]
        public IActionResult GetValidationOptions() => Ok(Validation.Options);

        [HttpGet("spec/entries/{businessId}")]
        public async Task<ActionResult<IEnumerable<DataSpecificationDto>>> GetEntries(Guid businessId)
        {
            return Collection(await 
                _dispatcher.Dispatch<GetBusinessDataSpecifications, IEnumerable<DataSpecificationDto>>(
                    new GetBusinessDataSpecifications(businessId)));
        }


        [HttpGet("spec/validators/")]
        public ActionResult<IEnumerable<string>> GetSpecificationValidators() =>
            Collection(Validation.Options);
    }
}
