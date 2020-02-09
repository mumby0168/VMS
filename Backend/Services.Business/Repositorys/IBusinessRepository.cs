using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Business.Domain;

namespace Services.Business.Repositorys
{
    public interface IBusinessRepository
    {
        Task Add(IBusiness business);

        Task<IEnumerable<IBusiness>> GetBusinessesAsync();
        Task<IBusiness> GetBusinessAsync(Guid queryId);
        Task UpdateAsync(IBusiness business);
        Task<bool> IsCodeInUseAsync(int number);
    }
}
