using System;
using System.Collections.Generic;
using Services.Common.Exceptions;
using Services.Visitors.Domain.Domain.Visitor;

namespace Services.Visitors.Domain.Aggregate
{
    public class VisitorAggregate : IVisitorAggregate
    {
        public VisitorDocument Create(Guid visitingUserId, Guid visitingBusinessId, Guid visitingSiteId, IEnumerable<VisitorData> visitorData)
        {
            return new VisitorDocument()
            {
                VisitingUserId = visitingUserId,
                VisitingBusinessId = visitingBusinessId,
                VisitingSiteId = visitingSiteId,
                Data = visitorData
            };
        }

        public VisitorData CreateData(Guid specId, string value)
        {
            return new VisitorData
            {
                DataSpecificationId = specId,
                Value = value
            };
        }
    }
}