using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public interface IAccessRecordRepository
    {
        Task AddAsync(IAccessRecordDocument recordDocument);
        Task<IEnumerable<IAccessRecordDocument>> GetForUser(Guid userId);
        Task<IEnumerable<IAccessRecordDocument>> GetForSite(Guid siteId);
        Task<IEnumerable<AccessRecordDocument>> GetForBusiness(Guid businessId);
        Task RemoveRangeByUserId(Guid userId);
    }
}
