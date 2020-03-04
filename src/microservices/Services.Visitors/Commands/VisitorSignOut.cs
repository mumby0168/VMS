using System;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Commands
{
    public class VisitorSignOut : ICommand
    {
        public Guid VisitorId { get; }

        public VisitorSignOut(Guid visitorId)
        {
            VisitorId = visitorId;
        }
    }
}