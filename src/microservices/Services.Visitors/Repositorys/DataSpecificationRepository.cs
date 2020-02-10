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
        private readonly IMongoRepository<DataSpecification> _repository;

        public DataSpecificationRepository(IMongoRepository<DataSpecification> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(IDataSpecification specification) =>
            _repository.AddAsync(specification as DataSpecification);

        public async Task<int> GetNextOrderNumberAsync(Guid businessId)
        {
            var items = await _repository.FindAsync(d => d.IsLive && d.BusinessId == businessId);
            var item = items.OrderByDescending(d => d.Order).FirstOrDefault();
            return item?.Order + 1 ?? 1;
        }

        public async Task<IEnumerable<IDataSpecification>> GetEntriesAsync(Guid businessId)
        {
            return await _repository.FindAsync(d => d.IsLive && d.BusinessId == businessId);
        }

        public Task UpdateAsync(IDataSpecification entry)
        {
            return _repository.UpdateAsync(entry as DataSpecification, entry.Id);
        }

        public Task RemoveAsync(IDataSpecification spec)
        {
            return _repository.RemoveAsync(spec.Id);
        }
    }
}
