using System;

namespace Services.Visitors.Domain
{
    public class VisitorData : IVisitorData
    {
        public Guid DataSpecificationId { get; private set; }

        public string Value { get; private set; }

        public VisitorData(Guid dataSpecId, string value)
        {
            DataSpecificationId = dataSpecId;
            Value = value;
        }
    }
}
