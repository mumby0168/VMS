using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Test
{
    [MicroService("Services.Mail")]
    public class TestIssueCommand : IEvent
    {
    }
}
