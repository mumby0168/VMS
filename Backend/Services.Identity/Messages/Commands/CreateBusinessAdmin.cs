using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Commands
{
    public class CreateBusinessAdmin : ICommand
    {
        public Guid BusinessId { get; }

        public string Email { get; }

        [JsonConstructor]
        public CreateBusinessAdmin(Guid businessId, string email)
        {
            BusinessId = businessId;
            Email = email;
        }
    }
}
