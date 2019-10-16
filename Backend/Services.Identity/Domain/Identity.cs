using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;
using Services.Common.Jwt;

namespace Services.Identity.Domain
{
    public class Identity : IDomain
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }

        public byte[] Hash { get; private set; }

        public byte[] Salt { get; private set; }

        public string Role { get; private set; }

        public Identity(string email, byte[] hash, byte[] salt, string role)
        {

            if (role != Roles.SystemAdmin)
            {

            }


            Id = new Guid();
            Email = email;
            Hash = hash;
            Salt = salt;
            Role = role;
        }

        public Identity()
        {
            
        }

    }
}
