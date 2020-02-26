using System;
namespace Services.Users.Domain
{
    public class UserStatusDocument : IUserStatusDocument
    {

        public UserStatusDocument()
        {

        }

        private UserStatusDocument(Guid userId, AccessAction action, Guid siteId)
        {
            UserId = userId;
            CurrentState = action;
            Updated = DateTime.Now;
            SiteId = siteId;
        }

        public Guid UserId { get; private set; }

        public Guid SiteId { get; private set; }

        public AccessAction CurrentState { get; private set; }

        public DateTime Updated { get; private set; }

        public Guid Id { get; private set; }

        public IUserStatusDocument Create(Guid userId, AccessAction action, Guid siteId)
        {
            return new UserStatusDocument(userId, action, siteId);
        }

        public void Update(AccessAction action, Guid siteId)
        {
            SiteId = siteId;
            CurrentState = action;
            Updated = DateTime.Now;
        }
    }
}
