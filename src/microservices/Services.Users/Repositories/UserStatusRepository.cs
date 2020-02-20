using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public class UserStatusRepository : IUserStatusRepository
    {
        private readonly IMongoRepository<UserStatus> _repository;

        public UserStatusRepository(IMongoRepository<UserStatus> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(IUserStatus status)
        {
            return _repository.AddAsync(status as UserStatus);
        }

        public async Task<IEnumerable<IUserStatus>> GetForSiteAsync(Guid siteId)
        {
            return await _repository.FindAsync(s => s.SiteId == siteId);
        }

        public async Task<IUserStatus> GetStatusForUserAsync(Guid userId)
        {
            return await _repository.GetAsync(s => s.UserId == userId);
        }

        public Task UpdateAsync(IUserStatus status)
        {
            return _repository.UpdateAsync(status as UserStatus, status.Id);
        }
    }
}
