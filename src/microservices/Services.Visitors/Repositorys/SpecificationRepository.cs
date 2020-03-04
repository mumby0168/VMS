using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Visitors.Domain;
using Services.Visitors.Domain.Domain.Specification;

namespace Services.Visitors.Repositorys
{
    public class SpecificationRepository : ISpecificationRepository
    {
        private readonly IMongoRepository<SpecificationDocument> _repository;

        public SpecificationRepository(IMongoRepository<SpecificationDocument> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(SpecificationDocument specificationDocument) =>
            _repository.AddAsync(specificationDocument as SpecificationDocument);

        public async Task<int> GetNextOrderNumberAsync(Guid businessId)
        {
            var items = await _repository.FindAsync(d => d.IsLive && d.BusinessId == businessId && d.IsMandatory == false);
            var item = items.OrderByDescending(d => d.Order).FirstOrDefault();
            return item?.Order + 1 ?? 2;
        }

        public async Task<IEnumerable<SpecificationDocument>> GetEntriesAsync(Guid businessId)
        {
            return await _repository.FindAsync(d => d.IsLive && d.BusinessId == businessId);
        }

        public Task UpdateAsync(SpecificationDocument entry)
        {
            return _repository.UpdateAsync(entry as SpecificationDocument, entry.Id);
        }

        public Task RemoveAsync(SpecificationDocument spec)
        {
            return _repository.RemoveAsync(spec.Id);
        }

        public async Task<Guid> GetNameSpecIdForBusinessAsync(Guid businessId)
        {
            var spec = await _repository.GetAsync(s => s.BusinessId == businessId && s.Label == "Full Name");
            return spec.Id;
        }
    }
}
