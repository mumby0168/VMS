using System;
using Services.Common.Domain;

namespace Services.Visitors.Domain.Domain
{
    public class VisitorDocument : IDomain
    {
        public Guid Id { get; internal set; }
        
        
    }
}