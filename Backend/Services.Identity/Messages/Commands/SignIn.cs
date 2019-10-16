using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Messages.Commands
{
    public class SignIn
    {
        public SignIn(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get;}

        public string Password { get;}
    }
}
