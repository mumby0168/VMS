using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Visitors.Domain
{

    public enum VisitorStatus : byte
    {
        In = 0,
        Out = 1,
    }

    public interface IVisitorDocument : IDomain
    {
        Guid VisitingUserId { get; }

        Guid VisitingBusinessId { get; }

        Guid VisitingSiteId { get; }

        VisitorStatus Status { get; }

        DateTime In { get; }

        DateTime? Out { get; }

        IEnumerable<IVisitorData> Data { get; }
    }
}
