using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Commands
{
    public class ResetPassword : ICommand
    {
        public Guid Code { get; }

        public string Email { get; }

        public string Password { get; }

        public string PasswordConfirm { get; }

        [JsonConstructor]
        public ResetPassword(Guid code, string email, string password, string passwordConfirm)
        {
            Code = code;
            Email = email;
            Password = password;
            PasswordConfirm = passwordConfirm;
        }
    }
}
