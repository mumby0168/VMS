using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Models
{
    public class AuthToken : IAuthToken
    {
        public string Jwt { get; }
        public string RefreshToken { get; }

        private AuthToken(string jwt, string refreshToken)
        {
            Jwt = jwt;
            RefreshToken = refreshToken;
        }

        private AuthToken() { }

        public static IAuthToken Create(string jwt, string refreshToken) => new AuthToken(jwt, refreshToken);
    }
}
