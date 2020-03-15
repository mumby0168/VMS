using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Domain;
using Services.Visitors.Domain.Domain.Specification;

namespace Services.Visitors.Repositorys
{
    public interface ISpecificationRepository
    {
        Task AddAsync(SpecificationDocument specificationDocument);

        Task<int> GetNextOrderNumberAsync(Guid businessId);
        Task<IEnumerable<SpecificationDocument>> GetLiveEntriesAsync(Guid businessId);
        Task UpdateAsync(SpecificationDocument entry);
        Task RemoveAsync(SpecificationDocument spec);
        Task<Guid> GetNameSpecIdForBusinessAsync(Guid businessId);
        Task<IEnumerable<SpecificationDocument>> GetEntriesForBusinessAsync(Guid visitorVisitingBusinessId);
    }
}
