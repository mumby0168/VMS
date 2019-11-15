using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Context
{
    public interface IUserContext
    {
        string Email { get; set; }

        Guid Id { get; set; }

        string Token { get; set; }
    }
}
