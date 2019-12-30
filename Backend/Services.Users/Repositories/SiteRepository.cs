using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private readonly IMongoRepository<Site> _repository;

        public SiteRepository(IMongoRepository<Site> repository)
        {
            _repository = repository;
        }
        public async Task<bool> IsSiteIdValid(Guid id)
        {
            var site = await _repository.GetAsync(id);
            return site != null;
        }

        public async Task<string> GetSiteNameAsync(Guid id)
        {
            var site = await _repository.GetAsync(id);
            return site?.Name;
        }

        public Task AddSiteAsync(ISite site)
        {
            return _repository.AddAsync(site as Site);
        }
    }
}
