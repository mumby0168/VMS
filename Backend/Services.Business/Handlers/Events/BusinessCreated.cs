﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Businesses.Handlers.Events
{
    public class BusinessCreated : IEvent
    {
        public BusinessCreated(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}