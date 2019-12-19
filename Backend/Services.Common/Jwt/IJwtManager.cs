using System;

namespace Services.Common.Jwt
{
    public interface IJwtManager
    {
        string CreateToken(Guid id, string email, string role);

        bool IsTokenValid(string encodedToken);
        string CreateToken(Guid identityId, string identityEmail, string identityRole, Guid identityBusinessId);
    }
}
