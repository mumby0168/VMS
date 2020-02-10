using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Sites.Messages.Queries
{
    public class GetSiteFireList : IQuery
    {
        public Guid SiteId { get; }

        public GetSiteFireList(Guid siteId)
        {
            SiteId = siteId;
        }
    }
}
