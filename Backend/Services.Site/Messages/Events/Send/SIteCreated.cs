﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Sites.Messages.Events.Send
{
    public class SiteCreated : IEvent
    {
        public Guid Id { get; }

        public string Name { get; }

        public SiteCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
