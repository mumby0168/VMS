using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Visitors.Domain
{
    public class VisitorDocument : IVisitorDocument
    {
        public Guid Id { get; private set; }
        public Guid VisitingUserId { get; private set; }
        public Guid VisitingBusinessId { get; private set; }
        public Guid VisitingSiteId { get; private set; }
        public VisitorStatus Status { get; private set; }
        public DateTime In { get; private set; }
        public DateTime? Out { get; private set; }
        public IEnumerable<IVisitorData> Data { get; private set; }

        public VisitorDocument(Guid visitingUserId, Guid visitingBusinessId, Guid visitingSiteId, IEnumerable<IVisitorData> data)
        {
            Id = Guid.NewGuid();
            VisitingUserId = visitingUserId;
            VisitingBusinessId = visitingBusinessId;
            VisitingSiteId = visitingSiteId;
            Status = VisitorStatus.In;
            In = DateTime.Now;
            Out = null;
            Data = data;
        }


    }
}
