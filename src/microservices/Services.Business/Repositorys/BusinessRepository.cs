using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Business.Domain;
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

        public Task Add(IBusiness business) => _repository.AddAsync(business as Domain.Business);
        public async Task<IEnumerable<IBusiness>> GetBusinessesAsync() => await _repository.GetAllAsync();
        public async Task<IBusiness> GetBusinessAsync(Guid id) => await _repository.GetAsync(id);
        public Task UpdateAsync(IBusiness business) => _repository.UpdateAsync(business as Domain.Business, business.Id);
        public async Task<bool> IsCodeInUseAsync(int number) => 
            await _repository.GetAsync(b => b.Code == number) != null;
    }
}
