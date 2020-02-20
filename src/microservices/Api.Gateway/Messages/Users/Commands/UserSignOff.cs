using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Messages.Users.Commands
{
    public class UserSignOff
    {
        public int Code { get; }

        public Guid SiteId { get; }

        public UserSignOff(int code, Guid siteId)
        {
            Code = code;
            SiteId = siteId;
        }
    }
}
