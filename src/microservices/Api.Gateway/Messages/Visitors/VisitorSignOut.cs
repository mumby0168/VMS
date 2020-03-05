using System;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Visitors
{
    [MicroService(Services.Common.Names.Services.Visitors)]
    public class VisitorSignOut : ICommand
    {
        public Guid VisitorId { get; }

        public VisitorSignOut(Guid visitorId)
        {
            VisitorId = visitorId;
        }
    }
}