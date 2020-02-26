using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Factories
{
    public class AccessRecordFactory : IAccessRecordFactory
    {
        public IAccessRecordDocument Create(Guid userId, Guid siteId, AccessAction action, Guid userBusinessId)
        {
            return new AccessRecordDocument().Create(userId, siteId, action, userBusinessId);
        }

        public IUserStatusDocument Create(Guid userId, Guid siteId, AccessAction action)
        {
            return new UserStatusDocument().Create(userId, action, siteId);
        }
    }
}
