using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Services.Business.Controllers
{

    [Route("business/status/api/")]
    public class BusinessStatusController : ControllerBase
    {
        [HttpGet("pulse")]
        public IActionResult Pulse()
        {
            return Ok();
        }
    }
}

