using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Services.Common.Jwt
{
    public interface IJwtFactory
    {
        SecurityTokenDescriptor CreateTokenDescriptor();

        ClaimsIdentity CreateClaimsIdentity(List<Claim> claims);

        Claim CreateClaim(string key, string value);    

        SigningCredentials CreateSigningCredentials(string secretKey, string alg);

        JwtSecurityTokenHandler CreateTokenHandler();
    }
}
