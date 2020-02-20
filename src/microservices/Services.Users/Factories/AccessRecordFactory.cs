using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Factories
{
    public class AccessRecordFactory : IAccessRecordFactory
    {
        public IAccessRecord Create(Guid userId, Guid siteId, AccessAction action, Guid userBusinessId)
        {
            return new AccessRecord().Create(userId, siteId, action, userBusinessId);
        }

        public IUserStatus Create(Guid userId, Guid siteId, AccessAction action)
        {
            return new UserStatus().Create(userId, action, siteId);
        }
    }
}
