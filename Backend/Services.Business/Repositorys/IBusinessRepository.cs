using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Business.Repositorys
{
    public interface IBusinessRepository
    {
        Task Add(Domain.Business business);

        Task<IEnumerable<Domain.Business>> GetBusinessesAsync();
    }
}
