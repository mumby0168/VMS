using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Services.Common.Jwt
{
    public interface IJwtToken
    {
        string RawToken { get; }

        string Role { get; }

        Guid Id { get; }

        IEnumerable<Claim> Claims { get; }

        DateTime Expiry { get; }

        Guid BusinessId { get; }
    }
}
