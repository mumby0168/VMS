using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Dtos.Sites;

namespace Api.Gateway.Clients.Interfaces
{
    public interface ISiteClient
    {
        Task<IEnumerable<SiteSummaryDto>> GetSites(Guid businessId);

        Task<SiteDto> GetSite(Guid siteId);

        Task<IEnumerable<SiteResourceDto>> GetResourcesForSite(Guid siteId);
    }
}
