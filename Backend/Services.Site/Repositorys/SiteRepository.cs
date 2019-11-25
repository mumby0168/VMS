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
        public Task AddAsync(ISite site)
        {
            return _repository.AddAsync(site as Site);
        }
    }
}
