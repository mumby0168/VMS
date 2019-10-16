using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.Identity.Messages.Commands;

namespace Services.Identity.Controllers
{
    [Route("api/admin/")]
    public class AdminController : ControllerBase
    {
        public AdminController()
        {

        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command)
        {
            return Ok();
        }

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAdmin command)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteAdmin command)
        {
            return Ok();
        }
    }
}
