using System;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Users.Events
{
    [MicroService(Common.Names.Services.Sites)]
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
