using Microsoft.AspNetCore.Mvc;

namespace Services.Sites.Controllers
{

    [Route("sites/status/api/")]
    public class SitesStatusController : ControllerBase
    {
        [HttpGet("pulse")]
        public IActionResult Pulse()
        {
            return Ok();
        }
    }
}

