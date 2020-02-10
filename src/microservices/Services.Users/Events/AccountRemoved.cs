using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Users.Events
{
    [MicroService(Common.Names.Services.Identity)]
    public class AccountRemoved : IEvent
    {
        [JsonConstructor]
        public AccountRemoved(Guid accountId)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; }
    }
}
