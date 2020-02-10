using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Users.Events
{
    public class AccessRecordCreated : IEvent
    {
    }
}
