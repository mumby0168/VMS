using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public interface IUserStatusRepository
    {
        Task<IUserStatusDocument> GetStatusForUserAsync(Guid userId);

        Task AddAsync(IUserStatusDocument statusDocument);

        Task UpdateAsync(IUserStatusDocument statusDocument);
        Task<IEnumerable<IUserStatusDocument>> GetForSiteAsync(Guid siteId);
    }
}
