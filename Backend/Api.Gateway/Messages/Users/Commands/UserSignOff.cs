using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Messages.Users.Commands
{
    public class UserSignOff
    {
        public Guid UserId { get; }

        public UserSignOff(Guid userId)
        {
            UserId = userId;
        }
    }
}
