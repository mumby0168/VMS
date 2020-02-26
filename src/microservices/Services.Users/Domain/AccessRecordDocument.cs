using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Users.Domain
{
    public class AccessRecordDocument : IAccessRecordDocument
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid SiteId { get; private set; }
        public Guid BusinessId { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public AccessAction Action { get; private set; }
        public IAccessRecordDocument Create(Guid userId, Guid siteId, AccessAction action, Guid businessId)
        {
            return new AccessRecordDocument(userId, siteId, action, businessId);
        }

        public AccessRecordDocument()
        {
            
        }

        private AccessRecordDocument(Guid userId, Guid siteId, AccessAction action, Guid businessId)
        {
            UserId = userId;
            SiteId = siteId;
            Action = action;
            BusinessId = businessId;
            TimeStamp = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
    }
}
