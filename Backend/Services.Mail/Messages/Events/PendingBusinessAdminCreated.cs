using System;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Events
{
    [MicroService(Common.Names.Services.Identity)]
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
