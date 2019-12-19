using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Messages.Users.Commands
{
    public class UserSignOn
    {
        public Guid UserId { get; }

        public UserSignOn(Guid userId)
        {
            UserId = userId;
        }
    }
}
