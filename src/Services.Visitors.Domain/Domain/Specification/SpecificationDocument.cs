using System;
using Services.Common.Domain;

namespace Services.Visitors.Domain.Domain.Specification
{
    public class SpecificationDocument : IDomain
    {
        public virtual Guid Id { get; internal set; }
        
        public string Label { get; internal set; }
        public virtual int Order { get; internal set; }
        public string ValidationMessage { get; internal set; }
        public string ValidationCode { get; internal set; }
        public bool IsLive { get; internal set; }

        public bool IsMandatory { get; internal set; }

        public Guid BusinessId { get; internal set; }
    }
}