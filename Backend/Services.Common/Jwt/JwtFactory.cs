using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Services.Common.Jwt
{
    public class JwtFactory : IJwtFactory
    {
        public SecurityTokenDescriptor CreateTokenDescriptor()
        {
            return new SecurityTokenDescriptor();
        }

        public ClaimsIdentity CreateClaimsIdentity(List<Claim> claims)
        {
            return new ClaimsIdentity(claims);
        }

        public Claim CreateClaim(string key, string value)
        {
            return new Claim(key, value);
        }

        public SigningCredentials CreateSigningCredentials(string secretKey, string alg)
        {
            return new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), alg);
        }

        public JwtSecurityTokenHandler CreateTokenHandler()
        {
            return new JwtSecurityTokenHandler();
        }
    }
}
