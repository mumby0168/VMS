using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Context
{
    public class UserContext : IUserContext
    {
        public string Email { get; set; }
        public Guid Id { get; set; }

        public  string Token { get; set; }
    }
}
