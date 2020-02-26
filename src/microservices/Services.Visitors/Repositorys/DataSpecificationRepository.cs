using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Visitors.Domain;

namespace Services.Visitors.Repositorys
{
    public class DataSpecificationRepository : IDataSpecificationRepository
    {
        private readonly IMongoRepository<DataSpecificationDocument> _repository;

        public DataSpecificationRepository(IMongoRepository<DataSpecificationDocument> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(IDataSpecificationDocument specificationDocument) =>
            _repository.AddAsync(specificationDocument as DataSpecificationDocument);

        public async Task<int> GetNextOrderNumberAsync(Guid businessId)
        {
            var items = await _repository.FindAsync(d => d.IsLive && d.BusinessId == businessId);
            var item = items.OrderByDescending(d => d.Order).FirstOrDefault();
            return item?.Order + 1 ?? 1;
        }

        public async Task<IEnumerable<IDataSpecificationDocument>> GetEntriesAsync(Guid businessId)
        {
            return await _repository.FindAsync(d => d.IsLive && d.BusinessId == businessId);
        }

        public Task UpdateAsync(IDataSpecificationDocument entry)
        {
            return _repository.UpdateAsync(entry as DataSpecificationDocument, entry.Id);
        }

        public Task RemoveAsync(IDataSpecificationDocument spec)
        {
            return _repository.RemoveAsync(spec.Id);
        }
    }
}
