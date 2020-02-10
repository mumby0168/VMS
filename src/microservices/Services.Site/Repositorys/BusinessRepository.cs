using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Sites.Domain;

namespace Services.Sites.Repositorys
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly IMongoRepository<Business> _repository;

        public BusinessRepository(IMongoRepository<Business> repository)
        {
            _repository = repository;
        }


        public Task AddAsync(IBusiness business)
        {
            return _repository.AddAsync(business as Business);
        }

        public async Task<bool> IsBusinessValidAsync(Guid id)
        {
            var business = await _repository.GetAsync(id);
            return business != null;
        }
    }
}
