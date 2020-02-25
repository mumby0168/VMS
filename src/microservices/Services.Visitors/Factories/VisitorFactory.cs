﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Domain;

namespace Services.Visitors.Factories
{
    public class VisitorFactory : IVisitorFactory
    {
        public IVisitor Create(Guid visitingUserId, Guid visitingBusinessId, Guid visitingSiteId, IEnumerable<IVisitorData> data)
        {
            return new Visitor(visitingUserId, visitingBusinessId, visitingSiteId, data);
        }
    }
}