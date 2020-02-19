using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(IUser user);

        Task<IUser> GetAsync(Guid id);
        Task<IUser> GetFromAccountId(Guid accountId);
        Task<IEnumerable<IUser>> GetUsersByBusinessId(Guid businessId);
        Task UpdateAsync(IUser user);
        Task<IUser> GetByCodeAsync(int code);
    }
}
