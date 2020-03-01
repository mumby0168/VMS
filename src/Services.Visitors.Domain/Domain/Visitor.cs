using System;
using Services.Common.Domain;

namespace Services.Visitors.Domain.Domain
{
    public class Visitor : IDomain
    {
        public Guid Id { get; internal set; }
        
        
    }
}