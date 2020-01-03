using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Domain;

namespace Services.Visitors.Repositorys
{
    public interface IDataSpecificationRepository
    {
        Task AddAsync(IDataSpecification specification);

        Task<int> GetNextOrderNumberAsync(Guid businessId);
        Task<IEnumerable<IDataSpecification>> GetEntriesAsync(Guid businessId);
        Task UpdateAsync(IDataSpecification entry);
    }
}
