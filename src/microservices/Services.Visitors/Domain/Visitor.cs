using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Visitors.Domain
{
    public class Visitor : IVisitor
    {
        public Guid Id { get; }
        public Guid UserId { get; }
    }
}
