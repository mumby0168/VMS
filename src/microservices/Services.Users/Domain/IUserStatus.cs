using System;
using Services.Common.Domain;

namespace Services.Users.Domain
{
    public interface IUserStatus : IDomain
    {
        Guid UserId { get; }

        AccessAction CurrentState { get; }

        Guid SiteId { get; }

        DateTime Updated { get; }

        IUserStatus Create(Guid userId, AccessAction action, Guid siteId);

        void Update(AccessAction action, Guid siteId);
    }
}
