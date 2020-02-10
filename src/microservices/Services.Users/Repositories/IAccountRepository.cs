using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public interface IAccountRepository
    {
        Task AddAsync(IAccount account);
        Task<IAccount> GetAsync(Guid accountId);
    }
}
