using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public class AccessRecordRepository : IAccessRecordRepository
    {
        private readonly IMongoRepository<AccessRecordDocument> _repository;

        public AccessRecordRepository(IMongoRepository<AccessRecordDocument> repository)
        {
            _repository = repository;
        }
        public Task AddAsync(IAccessRecordDocument recordDocument) => 
            _repository.AddAsync(recordDocument as AccessRecordDocument);

        public async Task<IEnumerable<IAccessRecordDocument>> GetForUser(Guid userId)
        {
            return await _repository.FindAsync(r => r.UserId == userId);
        }

        public async Task<IEnumerable<IAccessRecordDocument>> GetForSite(Guid siteId)
        {
            return await _repository.FindAsync(a => a.SiteId == siteId);
        }

        public async Task<IEnumerable<AccessRecordDocument>> GetForBusiness(Guid businessId)
        {
            return await _repository.FindAsync(a => a.BusinessId == businessId);
        }

        public Task RemoveRangeByUserId(Guid userId)
        {
            return _repository.RemoveRangeAsync(a => a.UserId == userId);
        }
    }
}
