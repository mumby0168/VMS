using Microsoft.AspNetCore.Mvc;

namespace Services.Operations.Controllers
{

    [Route("operations/status/api/")]
    public class OperationsStatusController : ControllerBase
    {
        [HttpGet("pulse")]
        public IActionResult Pulse()
        {
            return Ok();
        }
    }
}

