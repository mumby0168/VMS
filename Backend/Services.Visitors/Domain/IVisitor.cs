using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Visitors.Domain
{
    public interface IVisitor : IDomain
    {
        Guid UserId { get; }
    }
}
