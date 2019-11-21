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
        private readonly IAdminIdentityService _adminIdentityService;

        public AdminController(IAdminIdentityService adminIdentityService)
        {
            _adminIdentityService = adminIdentityService;
        }

        [AllowAnonymous]
        [HttpPost("sign-in-system")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command) => 
            Ok(await _adminIdentityService.SignIn(command.Email, command.Password, Roles.SystemAdmin));

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAdmin command)
        {
            await _adminIdentityService.CreateAdmin(command.Email);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteAdmin command)
        {
            await _adminIdentityService.CompleteAdmin(command.Code, command.Password, command.PasswordMatch, command.Email);
            return Ok();
        }

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("create-business-admin")]
        public async Task<IActionResult> Create([FromBody] CreateBusinessAdmin command)
        {
            await _adminIdentityService.CreateBusinessAdmin(command.Email, command.BusinessId);
            return Ok();
        }
    }
}
