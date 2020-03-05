using System;
using System.Collections.Generic;
using Services.Visitors.Domain.Domain.Visitor;

namespace Services.Visitors.Domain.Aggregate
{
    /// <summary>
    /// A service to manage any mutation in the VisitorDocument type,
    /// </summary>
    public interface IVisitorAggregate
    {
        VisitorDocument Create(Guid visitingUserId, Guid visitingBusinessId, Guid visitingSiteId,
            IEnumerable<VisitorData> visitorData);

        VisitorData CreateData(Guid specId, string value);
        void SignOut(VisitorDocument visitor);
    }
}