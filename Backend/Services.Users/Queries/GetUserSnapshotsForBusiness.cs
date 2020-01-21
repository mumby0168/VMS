using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Users.Queries
{
    public class GetUserSnapshotsForBusiness : IQuery
    {
        public Guid BusinessId { get; }

        public GetUserSnapshotsForBusiness(Guid businessId)
        {
            BusinessId = businessId;
        }
    }
}
