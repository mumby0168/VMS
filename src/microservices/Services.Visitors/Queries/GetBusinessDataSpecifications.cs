using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Visitors.Queries
{
    public class GetBusinessDataSpecifications : IQuery
    {
        public Guid BusinessId { get; }

        public GetBusinessDataSpecifications(Guid businessId)
        {
            BusinessId = businessId;
        }
    }
}
