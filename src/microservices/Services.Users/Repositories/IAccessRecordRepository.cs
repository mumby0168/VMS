using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public interface IAccessRecordRepository
    {
        Task AddAsync(IAccessRecord record);
        Task<IEnumerable<IAccessRecord>> GetForUser(Guid userId);
        Task<IEnumerable<IAccessRecord>> GetForSite(Guid siteId);
        Task<IEnumerable<AccessRecord>> GetForBusiness(Guid businessId);
        Task RemoveRangeByUserId(Guid userId);
    }
}
