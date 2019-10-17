using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Identity.Domain;

namespace Services.Identity.Repositorys
{
    public interface IPendingIdentityRepository
    {
        Task<bool> IsEmailInUse(string email);
        Task AddAsync(PendingIdentity pending);
        Task<PendingIdentity> GetAsync(Guid code, string email);
    }
}
