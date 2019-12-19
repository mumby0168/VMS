using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Common.Jwt;
using Services.Identity.Messages.Commands;
using Services.Identity.Services;

namespace Services.Identity.Controllers
{
    [Route("api/users/")]
    public class UserController : VmsControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command) =>
            Ok(await _userService.SignIn(command.Email, command.Password));

        [AllowAnonymous]
        [HttpPost("complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteUser command)
        {
            await _userService.CompleteUser(command.Code, command.Email, command.Password,
                command.PasswordConfirmation);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestReset([FromBody] RequestPasswordReset command)
        {
            await _userService.InitiatePasswordReset(command.Email);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword command)
        {
            await _userService.ResetPassword(command.Code, command.Email, command.Password, command.PasswordConfirm);
            return Ok();
        }
    }
}
