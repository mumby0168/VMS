using System;
using Services.Common.Domain;

namespace Services.Users.Domain
{
    public interface IUserStatus : IDomain
    {
        public Guid UserId { get; }

        public AccessAction CurrentState { get; }

        Guid SiteId { get; }

        public DateTime Updated { get; }

        public IUserStatus Create(Guid userId, AccessAction action, Guid siteId);

        public void Update(AccessAction action, Guid siteId);
    }
}
