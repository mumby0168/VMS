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

        public int Code { get; private set;}

        public IAccount Create(Guid id, string email, int code)
        {
            return new Account(id, email, code);
        }

        public Account()
        {


        }


        private Account(Guid id, string email, int code)
        {
            Id = id;
            Email = email;
            Code = code;
        }

    }
}
