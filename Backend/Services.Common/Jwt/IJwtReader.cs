using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Jwt
{
    public interface IJwtReader
    {
        string GetUserRole(string jwt);

        Guid GetUserId(string kwt);
    }
}
