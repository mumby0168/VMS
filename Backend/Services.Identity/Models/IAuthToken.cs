using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Models
{
    public interface IAuthToken
    {
        string Jwt { get; }

        string RefreshToken { get; }
    }
}
