using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Sites.Messages.Queries
{
    public class GetSiteResources : IQuery
    {
        public GetSiteResources(Guid siteId)
        {
            SiteId = siteId;
        }

        public Guid SiteId { get; }
    }
}
