using System;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Users.Events
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
