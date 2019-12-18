using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Services.Common.Mongo;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoRepository<Account> _collection;

        public AccountRepository(IMongoRepository<Account> collection)
        {
            _collection = collection;
        }
        public Task AddAsync(IAccount account)
        {
            return _collection.AddAsync(account as Account);
        }

        public async Task<IAccount> GetAsync(Guid accountId)
        {
            var account = await _collection.GetAsync(accountId);
            return account;
        }
    }
}
