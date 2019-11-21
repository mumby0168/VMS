using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Repositorys
{
    public interface IIdentityRepository
    {
        Task<bool> IsEmailInUse(string email, string role);

        Task<Domain.Identity> GetByEmailAndRole(string email, string role);
        Task AddAsync(Domain.Identity identity);
    }
}
