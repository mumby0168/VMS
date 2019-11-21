using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Events
{
    public class PendingBusinessAdminCreated : IEvent
    {
        public Guid Code { get; }

        public string Email { get; }

        public PendingBusinessAdminCreated(Guid code, string email)
        {
            Code = code;
            Email = email;
        }
    }
}
