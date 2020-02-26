using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Users.Domain
{
    public enum AccessAction
    {
        In,
        Out
}
    public interface IAccessRecordDocument : IDomain
    {
        Guid UserId { get; }

        Guid SiteId { get; }

        Guid BusinessId { get; }

        DateTime TimeStamp { get; }

        AccessAction Action { get; }

        IAccessRecordDocument Create(Guid userId, Guid siteId, AccessAction action, Guid businessId);
    }
}
