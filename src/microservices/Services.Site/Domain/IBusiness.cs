using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Sites.Domain
{
    public interface IBusiness : IDomain
    {
        IBusiness Create(Guid id);
    }
}
