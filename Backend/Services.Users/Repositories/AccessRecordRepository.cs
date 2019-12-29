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
        private readonly IMongoRepository<AccessRecord> _repository;

        public AccessRecordRepository(IMongoRepository<AccessRecord> repository)
        {
            _repository = repository;
        }
        public Task AddAsync(IAccessRecord record) => 
            _repository.AddAsync(record as AccessRecord);

        public async Task<IEnumerable<IAccessRecord>> GetForUser(Guid userId)
        {
            return await _repository.FindAsync(r => r.UserId == userId);
        }

        public Task<IEnumerable<IAccessRecord>> GetForSite(Guid siteId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccessRecord>> GetForBusiness(Guid businessId)
        {
            return await _repository.FindAsync(a => a.BusinessId == businessId);
        }
    }
}
