using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Users.Queries
{
    public class GetUsersOnSite : IQuery
    {
        public Guid SiteId { get; }

        public GetUsersOnSite(Guid siteId)
        {
            SiteId = siteId;
        }
    }
}
