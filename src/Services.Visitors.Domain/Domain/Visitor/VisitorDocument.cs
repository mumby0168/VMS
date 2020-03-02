using System;
using System.Collections.Generic;
using Services.Common.Domain;

namespace Services.Visitors.Domain.Domain.Visitor
{
    public class VisitorDocument : IDomain
    {
        public Guid Id { get; internal set; }
        public Guid VisitingUserId { get; internal set; }
        public Guid VisitingBusinessId { get; internal set; }
        public Guid VisitingSiteId { get; internal set; }
        public VisitorStatus Status { get; internal set; }
        public DateTime In { get; internal set; }
        public DateTime? Out { get; internal set; }
        public IEnumerable<VisitorData> Data { get; internal set; }
        
        internal VisitorDocument()
        {
            Id = Guid.NewGuid();
            In = DateTime.Now;
            Out = null;
            Status = VisitorStatus.In;
        }
        
        
    }
}