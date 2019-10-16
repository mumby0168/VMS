using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Services.Common.Jwt
{
    public class JwtManager : IJwtManager
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtManager(IJwtFactory jwtFactory, IConfiguration configuration)
        {
            _jwtFactory = jwtFactory;
            _configuration = configuration;
            _jwtSecurityTokenHandler = _jwtFactory.CreateTokenHandler();
        }
        public string CreateToken(Guid id, string email, string role)
        {
            var descriptor = _jwtFactory.CreateTokenDescriptor();
            var claims = new List<Claim>
            {
                _jwtFactory.CreateClaim(ClaimTypes.Email, email),
                _jwtFactory.CreateClaim(ClaimTypes.NameIdentifier, id.ToString()),
                _jwtFactory.CreateClaim(ClaimTypes.Role, role)
            };
            var identity = _jwtFactory.CreateClaimsIdentity(claims);
            descriptor.Subject = identity;
            descriptor.Expires = DateTime.Now.AddHours(3);
            descriptor.SigningCredentials = _jwtFactory.CreateSigningCredentials(_configuration.GetSection("jwt:secret").Value, SecurityAlgorithms.HmacSha512Signature);
            var token = _jwtSecurityTokenHandler.CreateToken(descriptor);
            return _jwtSecurityTokenHandler.WriteToken(token);
        }

        public bool IsTokenValid(string encodedToken)
        {
            var key = _configuration.GetSection("jwt:secret").Value;
            var tokenParams = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            _jwtSecurityTokenHandler.ValidateToken(encodedToken, tokenParams, out var token);

            return true;
        }
    }
}
