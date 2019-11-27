using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Sites.Messages.Queries
{
    public class GetSiteSummaries : IQuery
    {
        public Guid BusinessId { get; }

        public GetSiteSummaries(Guid businessId)
        {
            BusinessId = businessId;
        }
    }
}
