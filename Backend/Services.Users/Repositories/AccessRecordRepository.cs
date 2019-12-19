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

        public Task<IEnumerable<IAccessRecord>> GetForUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IAccessRecord>> GetForSite(Guid siteId)
        {
            throw new NotImplementedException();
        }
    }
}
