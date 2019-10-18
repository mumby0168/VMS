using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System_Admin.Services;

namespace System_Admin
{
    public class SystemAdminAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenStorageService _tokenStorageService;
        private readonly ILogger<SystemAdminAuthenticationStateProvider> _logger;

        public SystemAdminAuthenticationStateProvider(ITokenStorageService tokenStorageService, ILogger<SystemAdminAuthenticationStateProvider> logger)
        {
            this._tokenStorageService = tokenStorageService;
            this._logger = logger;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if(_tokenStorageService.Token is null) return Invalid();

            var role = _tokenStorageService.Token.Role;

            if(_tokenStorageService.Token.Expiry < DateTime.Now) Invalid();

            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, role) });

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        private Task<AuthenticationState> Invalid() => Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
    }
}