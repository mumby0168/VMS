using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Identity.Domain
{
    public class RefreshToken : IDomain
    {
        public Guid Id { get; private set; }
        public string Token { get; private set; }
        public string Email { get; private set; }
        public DateTime Expiry { get; private set; }

        public RefreshToken(string token, string email)
        {
            Id = Guid.NewGuid();
            Token = token;
            Email = email;
            Expiry = DateTime.Now.AddDays(1);
        }

        public RefreshToken()
        {
            
        }

    }
}
