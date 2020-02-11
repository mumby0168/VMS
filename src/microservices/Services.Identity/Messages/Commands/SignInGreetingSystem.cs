using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Commands
{

    public class SignInGreetingSystem : ICommand
    {
        public string Email { get; }

        public string Password { get; }

        public int Code { get; }

        [JsonConstructor]
        public SignInGreetingSystem(string email, string password, int code)
        {
            Email = email;
            Password = password;
            Code = code;
        }
    }
}
