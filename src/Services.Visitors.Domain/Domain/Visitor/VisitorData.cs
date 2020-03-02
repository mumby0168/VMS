using System;

namespace Services.Visitors.Domain.Domain.Visitor
{
    public class VisitorData
    {
        public Guid DataSpecificationId { get; internal set; }

        public string Value { get; internal set; }

        internal VisitorData()
        {
        }
    }
}