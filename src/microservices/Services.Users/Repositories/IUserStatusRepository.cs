using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public interface IUserStatusRepository
    {
        Task<IUserStatus> GetStatusForUserAsync(Guid userId);

        Task AddAsync(IUserStatus status);

        Task UpdateAsync(IUserStatus status);
        Task<IEnumerable<IUserStatus>> GetForSiteAsync(Guid siteId);
    }
}
