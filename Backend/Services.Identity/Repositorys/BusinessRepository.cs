using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Identity.Domain;

namespace Services.Identity.Repositorys
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly IMongoRepository<Business> _repository;

        public BusinessRepository(IMongoRepository<Business> repository)
        {
            _repository = repository;
        }


        public async Task<bool> ContainsBusinessAsync(Guid business) => await _repository.GetAsync(business) != null;
    }
}
