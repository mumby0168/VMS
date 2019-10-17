using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.Identity.Messages.Commands;
using Services.Identity.Services;

namespace Services.Identity.Controllers
{
    [Route("api/admin/")]
    public class AdminController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AdminController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost("sign-in-system")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command) => 
            Ok(await _identityService.SignIn(command.Email, command.Password, Roles.SystemAdmin));

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAdmin command)
        {
            await _identityService.CreateAdmin(command.Email);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteAdmin command)
        {
            await _identityService.CompleteAdmin(command.Code, command.Password, command.PasswordMatch, command.Email);
            return Ok();
        }
    }
}
