using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;
using Services.Common.Exceptions;
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

        public Guid BusinessId { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public int Code {get; private set;}

        public Identity(string email, byte[] hash, byte[] salt, string role)
        {

            if (role != Roles.SystemAdmin)
            {

            }


            Id = new Guid();
            BusinessId = Guid.Empty;
            Email = email;
            Hash = hash;
            Salt = salt;
            Role = role;
            CreatedOn = DateTime.UtcNow;
            Code = 0;
        }

        public Identity(string email, byte[] hash, byte[] salt, string role, Guid businessId, int code)
        {
            if(role == Roles.SystemAdmin)
            {
                throw new VmsException(Codes.InvalidCredentials, "This type of user is invalid with this identity type.");
            }
            BusinessId = businessId;
            Id = new Guid();
            Email = email;
            Hash = hash;
            Salt = salt;
            Role = role;
            CreatedOn = DateTime.UtcNow;
            Code = code;
        }

        public void UpdatePassword(byte[] hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public Identity()
        {
            
        }

    }
}
