using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Sites.Domain;

namespace Services.Sites.Repositorys
{
    public interface ISiteResourceRepository
    {
        Task AddAsync(ISiteResource resource);

        Task RemoveAsync(Guid resourceId);

        Task<IEnumerable<ISiteResource>> GetSiteResources(Guid siteId)
    }
}
