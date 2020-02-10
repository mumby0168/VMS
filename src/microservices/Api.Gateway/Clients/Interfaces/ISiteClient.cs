using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Dtos.Sites;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.Clients.Interfaces
{
    public interface ISiteClient
    {
        Task<IEnumerable<SiteSummaryDto>> GetSites(Guid businessId);

        Task<SiteDto> GetSite(Guid siteId);

        Task<IEnumerable<SiteResourceDto>> GetResourcesForSite(Guid siteId);

        Task<SiteAvailabilityDto> GetSiteAvailabilityAsync(Guid siteId);
        Task<SiteFireListDto> GetFireListForSiteAsync(Guid siteId);
    }
}
        