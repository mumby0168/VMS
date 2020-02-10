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
        public bool IsLoggedIn { get; set; }

        public void Clear()
        {
            IsLoggedIn = false;
            Email = string.Empty;
            Id = Guid.Empty;
            Token = string.Empty;
        }
    }
}
