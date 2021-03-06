﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Events
{
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
