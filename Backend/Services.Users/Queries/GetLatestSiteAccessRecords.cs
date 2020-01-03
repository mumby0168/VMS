using System;
using Services.Common.Queries;

namespace Services.Users.Queries
{
    public class GetLatestSiteAccessRecords : IQuery
    {
        public Guid SiteId { get; }

        public GetLatestSiteAccessRecords(Guid siteId)
        {
            SiteId = siteId;
        }
    }
}
