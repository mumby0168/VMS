using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Domain;

namespace Services.Visitors.Repositorys
{
    public interface IDataSpecificationRepository
    {
        Task AddAsync(IDataSpecificationDocument specificationDocument);

        Task<int> GetNextOrderNumberAsync(Guid businessId);
        Task<IEnumerable<IDataSpecificationDocument>> GetEntriesAsync(Guid businessId);
        Task UpdateAsync(IDataSpecificationDocument entry);
        Task RemoveAsync(IDataSpecificationDocument spec);
    }
}
