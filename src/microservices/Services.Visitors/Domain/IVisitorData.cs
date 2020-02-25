using System;

namespace Services.Visitors.Domain
{
    public interface IVisitorData
    {
        Guid DataSpecificationId { get; }

        string Value { get; }
    }
}
