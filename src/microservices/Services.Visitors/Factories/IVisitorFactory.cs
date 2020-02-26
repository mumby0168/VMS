using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Domain;

namespace Services.Visitors.Factories
{
    interface IVisitorFactory
    {
        IVisitorDocument Create(Guid visitingUserId, Guid visitingBusinessId, Guid visitingSiteId,
            IEnumerable<IVisitorData> data);
    }
}
