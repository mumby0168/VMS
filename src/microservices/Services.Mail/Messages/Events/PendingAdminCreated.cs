using System;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Events
{
    [MicroService("Services.Identity")]
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