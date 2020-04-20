using System;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Events
{
    [MicroService(Common.Names.Services.Identity)]
    public class UserAccountCreated : IEvent
    {
        public Guid Id { get; }

        public string Email { get; }

        public int Code { get; }

        [JsonConstructor]
        public UserAccountCreated(Guid id, string email, int code)
        {
            Id = id;
            Email = email;
            Code = code;
        }
    }
}
