using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Users.Queries
{
    public class GetBusinessAccessRecords : IQuery
    {
        public Guid BusinessId { get; }

        public GetBusinessAccessRecords(Guid businessId)
        {
            BusinessId = businessId;
        }
    }
}
