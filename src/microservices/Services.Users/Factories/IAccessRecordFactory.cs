using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Factories
{
    public interface IAccessRecordFactory
    {
        IAccessRecord Create(Guid userId, Guid siteId, AccessAction action, Guid userBusinessId);

        IUserStatus Create(Guid userId, Guid siteId, AccessAction action);
    }
}
