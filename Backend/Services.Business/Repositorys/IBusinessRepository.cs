using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Business.Repositorys
{
    public interface IBusinessRepository
    {
        Task Add(Domain.Business business);

        Task<IEnumerable<Domain.Business>> GetBusinessesAsync();
        Task<Domain.Business> GetBusinessAsync(Guid queryId);
        Task UpdateAsync(Domain.Business business);
    }
}
