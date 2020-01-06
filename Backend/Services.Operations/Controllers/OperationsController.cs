using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Operations;
using Services.Common.Base;
using Services.Operations.Dtos;
using Services.Operations.Services;

namespace Services.Operations.Controllers
{
    [Route("operations/api/")]
    public class OperationsController : VmsControllerBase
    {
        private readonly IOperationsCache _operationsCache;

        public OperationsController(IOperationsCache operationsCache)
        {
            _operationsCache = operationsCache;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDto>> Get([FromRoute]Guid id)
        {
            return Single(await _operationsCache.GetAsync(id));
        }

    }
}
