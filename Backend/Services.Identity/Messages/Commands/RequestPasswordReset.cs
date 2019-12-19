using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Commands
{
    public class RequestPasswordReset : ICommand
    {
        public string Email { get; }

        [JsonConstructor]
        public RequestPasswordReset(string email)
        {
            Email = email;
        }   
    }
}
