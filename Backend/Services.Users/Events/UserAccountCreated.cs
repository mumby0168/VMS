using System;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Users.Events
{
    [MicroService(ServiceNames.Identity)]
    public class UserAccountCreated : IEvent
    {
        public Guid Id { get; }

        public string Email { get; }

        public UserAccountCreated(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
