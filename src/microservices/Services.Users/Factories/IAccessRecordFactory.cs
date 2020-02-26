using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Factories
{
    public interface IAccessRecordFactory
    {
        IAccessRecordDocument Create(Guid userId, Guid siteId, AccessAction action, Guid userBusinessId);

        IUserStatusDocument Create(Guid userId, Guid siteId, AccessAction action);
    }
}
