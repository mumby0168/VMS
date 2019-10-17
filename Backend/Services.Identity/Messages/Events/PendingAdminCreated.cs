using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Events
{
    public class PendingAdminCreated : IEvent
    {
        public Guid Code { get; }

        public string Email { get; }

        [JsonConstructor]
        public PendingAdminCreated(Guid code, string email)
        {
            Code = code;
            Email = email;
        }

        public PendingAdminCreated()
        {
            
        }
    }
}
