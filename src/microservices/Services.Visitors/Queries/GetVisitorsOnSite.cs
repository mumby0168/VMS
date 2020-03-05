using System;
using Services.Common.Queries;

namespace Services.Visitors.Queries
{
    public class GetVisitorsOnSite : IQuery
    {
        public Guid SiteId { get; }

        
        public GetVisitorsOnSite(Guid siteId)
        {
            SiteId = siteId;
        }
    }
}