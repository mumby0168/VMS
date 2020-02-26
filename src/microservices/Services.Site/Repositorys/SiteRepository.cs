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
        private readonly IMongoRepository<SiteDocument> _repository;

        public SiteRepository(IMongoRepository<SiteDocument> repository)
        {
            _repository = repository;
        }


        public Task AddAsync(ISiteDocument siteDocument) => 
            _repository.AddAsync(siteDocument as SiteDocument);

        public async Task<ISiteDocument> GetAsync(Guid id) => 
            await _repository.GetAsync(id);

        public async Task<IEnumerable<ISiteDocument>> GetSitesForBusinessAsync(Guid businessId) =>
            await _repository.FindAsync(s => s.BusinessId == businessId);

        public Task Update(ISiteDocument siteDocument) => _repository.UpdateAsync(siteDocument as SiteDocument, siteDocument.Id);
        public async Task<bool> IsSiteIdValid(Guid siteId)
        {
            var site = await _repository.GetAsync(siteId);
            return site != null;
        }
    }
}
