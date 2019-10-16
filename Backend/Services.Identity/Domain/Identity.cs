using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;
using Services.Common.Jwt;

namespace Services.Identity.Domain
{
    public class Identity : IIdentifiable
    {
        public Guid Id { get; }
        public string Email { get; }

        public byte[] Hash { get; }

        public byte[] Salt { get; }

        public string Role { get; }

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

    }
}
