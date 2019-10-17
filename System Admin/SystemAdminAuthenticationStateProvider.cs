using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System_Admin.Services;

namespace System_Admin
{
    public class SystemAdminAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenStorageService _tokenStorageService;

        public SystemAdminAuthenticationStateProvider(ITokenStorageService tokenStorageService)
        {
            this._tokenStorageService = tokenStorageService;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if(_tokenStorageService.Token is null) return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));

            var role = _tokenStorageService.Token.Role;

            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, role) });

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }
    }
}