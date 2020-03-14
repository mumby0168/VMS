using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Visitors.Dtos;

namespace Services.Visitors.Repositorys
{
    public interface ISiteServiceClient
    {
        Task<SiteDto> GetSiteAsync(Guid siteId);
        Task<IEnumerable<SiteDto>> GetSitesForBusinessAsync(Guid businessId);
    }
}