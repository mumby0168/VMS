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
        private readonly IMongoRepository<UserDocument> _repository;

        public UserRepository(IMongoRepository<UserDocument> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(IUserDocument userDocument)
        {
            return _repository.AddAsync(userDocument as UserDocument);
        }

        public async Task<IUserDocument> GetAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);
            return user;
        }

        public async Task<IUserDocument> GetByCodeAsync(int code) => await _repository.GetAsync(u => u.Code == code);

        public async Task<IUserDocument> GetFromAccountId(Guid accountId)
        {
            return await _repository.GetAsync(u => u.AccountId == accountId);
        }

        public async Task<IEnumerable<IUserDocument>> GetUsersByBusinessId(Guid businessId) 
            => await _repository.FindAsync(u => u.BusinessId == businessId && u.IsAccountValid);
        

        public Task UpdateAsync(IUserDocument userDocument) => 
            _repository.UpdateAsync(userDocument as UserDocument, userDocument.Id);
    }
}
