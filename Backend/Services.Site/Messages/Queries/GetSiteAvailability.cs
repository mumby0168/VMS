using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Sites.Messages.Queries
{
    public class GetSiteAvailability : IQuery
    {
        public Guid SiteId { get; }

        public GetSiteAvailability(Guid siteId)
        {
            SiteId = siteId;
        }
    }
}
