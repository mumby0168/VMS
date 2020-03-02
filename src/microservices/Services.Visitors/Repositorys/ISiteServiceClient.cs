using System;
using System.Threading.Tasks;
using Services.Visitors.Dtos;

namespace Services.Visitors.Repositorys
{
    public interface ISiteServiceClient
    {
        Task<SiteDto> GetSiteAsync(Guid siteId);
    }
}