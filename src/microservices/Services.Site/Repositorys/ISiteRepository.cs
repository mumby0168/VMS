using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Services.Sites.Domain;

namespace Services.Sites.Repositorys
{
    public interface ISiteRepository
    {
        Task AddAsync(ISiteDocument siteDocument);
        Task<ISiteDocument> GetAsync(Guid id);
        Task<IEnumerable<ISiteDocument>> GetSitesForBusinessAsync(Guid businessId);
        Task Update(ISiteDocument siteDocument);
        Task<bool> IsSiteIdValid(Guid siteId);
    }
}
    