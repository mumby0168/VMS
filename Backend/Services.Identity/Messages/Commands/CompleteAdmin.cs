using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Commands
{
    public class CompleteAdmin : ICommand
    {
        public Guid Code { get; }

        public string Email { get; }

        public string Password { get; }

        public string PasswordMatch { get; }

        [JsonConstructor]
        public CompleteAdmin(Guid code, string password, string passwordMatch, string email)
        {
            Code = code;
            Password = password;
            PasswordMatch = passwordMatch;
            Email = email;
        }
    }
}
