using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Sites.Dtos;

namespace Services.Sites.Services
{
    public interface ISystemServices
    {
        Task<IEnumerable<LatestAccessRecordDto>> GetLatestAccessRecordsForSite(Guid siteId);
        Task<IEnumerable<OnSiteAccessRecordDto>> GetUsersOnSiteAsync(Guid querySiteId);
    }
}
