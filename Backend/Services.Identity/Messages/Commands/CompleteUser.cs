using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Commands
{
    public class CompleteUser : ICommand
    {
        public Guid Code { get; }

        public string Password { get; }

        public string PasswordConfirmation { get; }

        public string Email { get; }

        public CompleteUser(Guid code, string password, string passwordConfirmation, string email)
        {
            Code = code;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            Email = email;
        }
    }
}
