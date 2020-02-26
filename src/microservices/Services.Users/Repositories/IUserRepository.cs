using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(IUserDocument userDocument);

        Task<IUserDocument> GetAsync(Guid id);
        Task<IUserDocument> GetFromAccountId(Guid accountId);
        Task<IEnumerable<IUserDocument>> GetUsersByBusinessId(Guid businessId);
        Task UpdateAsync(IUserDocument userDocument);
        Task<IUserDocument> GetByCodeAsync(int code);
        
    }
}
