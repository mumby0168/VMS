using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Domain
{
    public class ResetRequest : IResetRequest
    {
        public Guid Id { get; private set; }

        public string Email { get; private set; }
        public DateTime RequestedAt { get; private set; }

        public IResetRequest Create(string email)
        {
            return new ResetRequest(email);
        }

        public ResetRequest()
        {
            
        }

        private ResetRequest(string email)
        {
            RequestedAt = DateTime.UtcNow;
            Id = Guid.NewGuid();
            Email = email;
        }
    }
}
