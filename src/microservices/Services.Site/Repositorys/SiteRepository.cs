using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Sites.Domain;

namespace Services.Sites.Repositorys
{
    public class SiteRepository : ISiteRepository
    {
        private readonly IMongoRepository<Site> _repository;

        public SiteRepository(IMongoRepository<Site> repository)
        {
            _repository = repository;
        }


        public Task AddAsync(ISite site) => 
            _repository.AddAsync(site as Site);

        public async Task<ISite> GetAsync(Guid id) => 
            await _repository.GetAsync(id);

        public async Task<IEnumerable<ISite>> GetSitesForBusinessAsync(Guid businessId) =>
            await _repository.FindAsync(s => s.BusinessId == businessId);

        public Task Update(ISite site) => _repository.UpdateAsync(site as Site, site.Id);
        public async Task<bool> IsSiteIdValid(Guid siteId)
        {
            var site = await _repository.GetAsync(siteId);
            return site != null;
        }
    }
}
