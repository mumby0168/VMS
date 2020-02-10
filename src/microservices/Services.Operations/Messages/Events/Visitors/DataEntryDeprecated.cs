using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Visitors
{
    [MicroService(Common.Names.Services.Visitors)]
    public class DataEntryDeprecated : IEvent
    {
    }
}
