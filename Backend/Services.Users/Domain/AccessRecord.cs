using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Users.Domain
{
    public class AccessRecord : IAccessRecord
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid SiteId { get; }
        public Guid BusinessId { get; }
        public DateTime TimeStamp { get; }
        public AccessAction Action { get; }
        public IAccessRecord Create(Guid userId, Guid siteId, AccessAction action, Guid businessId)
        {
            return new AccessRecord(userId, siteId, action, businessId);
        }

        public AccessRecord()
        {
            
        }

        private AccessRecord(Guid userId, Guid siteId, AccessAction action, Guid businessId)
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
