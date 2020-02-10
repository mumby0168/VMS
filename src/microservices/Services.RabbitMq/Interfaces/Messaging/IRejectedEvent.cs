using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public interface IRejectedEvent : IEvent
    {
        string Code { get; }

        string Reason { get; }
    }
}
