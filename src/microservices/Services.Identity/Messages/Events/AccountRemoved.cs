using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Events
{
    public class AccountRemoved : IEvent
    {
        public Guid AccountId { get; }
        
        [JsonConstructor]
        public AccountRemoved(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
    