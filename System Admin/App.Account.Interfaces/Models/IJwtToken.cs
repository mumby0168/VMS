using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Account.Interfaces.Models {
    public interface IJwtToken {
        string RawToken { get; }

        string Role { get; }

        IEnumerable<Claim> Claims { get; }

        DateTime Expiry { get; }

        Guid Id { get;  }

        string Email { get;  }
    }
}