using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity
{
    public static class Codes
    {
        public const string EmailInUse = "email_in_use";

        public const string InvalidCode = "invalid_code";

        public const string InvalidPasswords = "invalid_passwords";

        public const string InvalidEmail = "invalid_email";

        public const string InvalidCredentials = "invalid_credentials";

        public const string NoRefreshToken = "no_refresh_token_found";

        public const string BusinessNotFound = "busness_not_found";
    }
}
