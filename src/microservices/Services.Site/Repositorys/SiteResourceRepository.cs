using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Sites.Domain;

namespace Services.Sites.Repositorys
{
    public class SiteResourceRepository : ISiteResourceRepository
    {
        private readonly IMongoRepository<SiteResource> _repository;

        public SiteResourceRepository(IMongoRepository<SiteResource> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(ISiteResource resource) => _repository.AddAsync(resource as SiteResource);

        public Task RemoveAsync(Guid resourceId) => _repository.RemoveAsync(resourceId);

        public async Task<IEnumerable<ISiteResource>> GetSiteResources(Guid siteId) => await _repository.FindAsync(r => r.SiteId == siteId);
    }
}
