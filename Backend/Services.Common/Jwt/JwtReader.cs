using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Services.Common.Jwt
{
    public class JwtReader : IJwtReader
    {
        private readonly JwtSecurityTokenHandler _handler;

        public JwtReader(JwtSecurityTokenHandler handler)
        {
            _handler = handler;
        }
        public string GetUserRole(string jwt) => _handler.ReadJwtToken(jwt).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        public Guid GetUserId(string jwt)
        {
            string value = _handler.ReadJwtToken(jwt).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            return value == null ? Guid.Empty : Guid.Parse(value);
        }
    }
}
