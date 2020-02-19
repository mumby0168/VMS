using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Messages.Users.Commands
{
    public class UserSignOn
    {
        public int Code { get; }

        public UserSignOn(int code)
        {
            Code = code;
        }
    }
}
