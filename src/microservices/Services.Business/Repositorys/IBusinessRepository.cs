using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Business.Domain;

namespace Services.Business.Repositorys
{
    public interface IBusinessRepository
    {
        Task Add(IBusinessDocument businessDocument);

        Task<IEnumerable<IBusinessDocument>> GetBusinessesAsync();
        Task<IBusinessDocument> GetBusinessAsync(Guid queryId);
        Task UpdateAsync(IBusinessDocument businessDocument);
        Task<bool> IsCodeInUseAsync(int number);
    }
}
