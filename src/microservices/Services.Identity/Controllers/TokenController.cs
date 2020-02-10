using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Services.Identity.Messages.Commands;
using Services.Identity.Services;

namespace Services.Identity.Controllers
{
    [Route("api/tokens/")]
    public class TokenController : Controller
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public TokenController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        [Authorize]
        [HttpPost("revoke")]
        public async Task<AcceptedResult> Revoke([FromBody]RevokeRefreshToken command)
        {
            await _refreshTokenService.RevokeToken(command.Token,
                HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);

            return Accepted();
        }

        [Authorize]
        [HttpPost("validate")]
        public Task Validate() 
            => Task.FromResult(Ok());
    }
}