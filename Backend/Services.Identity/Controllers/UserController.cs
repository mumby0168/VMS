using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Common.Exceptions;
using Services.Common.Jwt;
using Services.Identity.Dtos;
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

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserAccount command)
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == CustomClaims.BusinessIdClaim);
            if (id == null) return Unauthorized();
            await _userService.CreateUser(command.Email, Guid.Parse(id.Value));
            return Ok();
        }

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpGet("accounts")]
        public async Task<ActionResult<IEnumerable<StandardUserAccountDto>>> GetAccounts()
        {
            var businessId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == CustomClaims.BusinessIdClaim);
            if (businessId is null) return Unauthorized();
            var collection = await Task.Run(() => _userService.GetStandardAccountsForBusiness(Guid.Parse(businessId.Value)));
            return Collection(collection.ToEnumerable());
        }

        [Authorize(Roles = Roles.BusinessAdmin)]
        [HttpPost("remove/{accountId}")]
        public async Task<IActionResult> DeleteAccount([FromRoute]Guid accountId)
        {
            var businessId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == CustomClaims.BusinessIdClaim);
            if (businessId is null) return Unauthorized();
            await _userService.RemoveAsync(accountId, Guid.Parse(businessId.Value));
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
