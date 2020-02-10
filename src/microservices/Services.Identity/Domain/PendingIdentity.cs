using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;
using Services.Common.Exceptions;
using Services.Common.Validation;

namespace Services.Identity.Domain
{
    public class PendingIdentity : IDomain
    {
        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public string Role { get; private set; }

        public Guid BusinessId { get; private set; }


        public PendingIdentity(Guid id, string email, string role)
        {
            Validate(email);
            Id = id;
            Role = role;
            Email = email;
        }

        public PendingIdentity(Guid id, string email, string role, Guid businessId)
        {
            Validate(email);
            Id = id;
            Role = role;
            Email = email;
            BusinessId = businessId;
        }

        private void Validate(string email)
        {
            if(!Validator.IsEmailValid(email)) throw new VmsException(Codes.InvalidEmail, $"The email {email} is not valid.");
        }

        public PendingIdentity()
        {
            
        }
    }
}
