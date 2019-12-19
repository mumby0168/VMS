using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Identity.Domain;

namespace Services.Identity.Repositorys
{
    public interface IResetRequestRepository
    {
        Task AddAsync(IResetRequest request);
        Task<IResetRequest> GetAsync(Guid code);
    }
}
