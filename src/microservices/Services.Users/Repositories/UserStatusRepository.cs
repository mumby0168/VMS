using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public class UserStatusRepository : IUserStatusRepository
    {
        private readonly IMongoRepository<UserStatusDocument> _repository;

        public UserStatusRepository(IMongoRepository<UserStatusDocument> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(IUserStatusDocument statusDocument)
        {
            return _repository.AddAsync(statusDocument as UserStatusDocument);
        }

        public async Task<IEnumerable<IUserStatusDocument>> GetForSiteAsync(Guid siteId)
        {
            return await _repository.FindAsync(s => s.SiteId == siteId);
        }

        public async Task<IUserStatusDocument> GetStatusForUserAsync(Guid userId)
        {
            return await _repository.GetAsync(s => s.UserId == userId);
        }

        public Task UpdateAsync(IUserStatusDocument statusDocument)
        {
            return _repository.UpdateAsync(statusDocument as UserStatusDocument, statusDocument.Id);
        }
    }
}
