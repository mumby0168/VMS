using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;

        public UserRepository(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(IUser user)
        {
            return _repository.AddAsync(user as User);
        }

        public async Task<IUser> GetAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);
            return user;
        }

        public async Task<IUser> GetByCodeAsync(int code) => await _repository.GetAsync(u => u.Code == code);

        public async Task<IUser> GetFromAccountId(Guid accountId)
        {
            return await _repository.GetAsync(u => u.AccountId == accountId);
        }

        public async Task<IEnumerable<IUser>> GetUsersByBusinessId(Guid businessId) 
            => await _repository.FindAsync(u => u.BusinessId == businessId && u.IsAccountValid);

        public Task UpdateAsync(IUser user) => 
            _repository.UpdateAsync(user as User, user.Id);
    }
}
