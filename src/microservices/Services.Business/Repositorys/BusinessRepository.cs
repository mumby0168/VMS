using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Business.Domain;
using Services.Common.Mongo;

namespace Services.Business.Repositorys
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly IMongoRepository<Domain.BusinessDocument> _repository;

        public BusinessRepository(IMongoRepository<Domain.BusinessDocument> repository)
        {
            _repository = repository;
        }

        public Task Add(IBusinessDocument businessDocument) => _repository.AddAsync(businessDocument as Domain.BusinessDocument);
        public async Task<IEnumerable<IBusinessDocument>> GetBusinessesAsync() => await _repository.GetAllAsync();
        public async Task<IBusinessDocument> GetBusinessAsync(Guid id) => await _repository.GetAsync(id);
        public Task UpdateAsync(IBusinessDocument businessDocument) => _repository.UpdateAsync(businessDocument as Domain.BusinessDocument, businessDocument.Id);
        public async Task<bool> IsCodeInUseAsync(int number) => 
            await _repository.GetAsync(b => b.Code == number) != null;
    }
}
