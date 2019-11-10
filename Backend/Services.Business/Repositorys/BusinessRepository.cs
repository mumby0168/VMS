using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Businesses.Domain;
using Services.Common.Mongo;

namespace Services.Businesses.Repositorys
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly IMongoRepository<Business> _repository;

        public BusinessRepository(IMongoRepository<Business> repository)
        {
            _repository = repository;
        }

        public Task Add(Business business) => _repository.AddAsync(business);
    }
}
