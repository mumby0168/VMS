using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Users.Domain
{
    public class Account : IAccount
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public IAccount Create(Guid id, string email)
        {
            return new Account(id, email);
        }

        public Account()
        {


        }


        private Account(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

    }
}
