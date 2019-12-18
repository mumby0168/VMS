using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Jwt
{
    public static class Roles
    {
        public const string SystemAdmin = "SystemAdmin";

        public const string BusinessAdmin = "BusinessAdmin";

        public const string StandardPortalUser = "Standard";

        public const string PortalUser = "BusinessAdmin, Standard";
    }
}
