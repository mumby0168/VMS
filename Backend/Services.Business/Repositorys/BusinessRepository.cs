using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Common.Mongo;

namespace Services.Business.Repositorys
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly IMongoRepository<Domain.Business> _repository;

        public BusinessRepository(IMongoRepository<Domain.Business> repository)
        {
            _repository = repository;
        }

        public Task Add(Domain.Business business) => _repository.AddAsync(business);
        public Task<IEnumerable<Domain.Business>> GetBusinessesAsync() => _repository.GetAllAsync();
        public Task<Domain.Business> GetBusinessAsync(Guid id) => _repository.GetAsync(id);
    }
}
