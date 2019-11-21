using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Services.Common.Mongo;
using Services.Identity.Domain;

namespace Services.Identity.Repositorys
{
    public class PendingIdentityRepository : IPendingIdentityRepository
    {
        private readonly IMongoRepository<PendingIdentity> _repository;

        public PendingIdentityRepository(IMongoRepository<PendingIdentity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsEmailInUse(string email, string role) => 
            await _repository.GetAsync(i => i.Email == email && i.Role == role) != null;

        public Task AddAsync(PendingIdentity pending) => 
            _repository.AddAsync(pending);

        public Task<PendingIdentity> GetAsync(Guid code, string email) =>
            _repository.GetAsync(p => p.Email == email && p.Id == code);

        public Task<IEnumerable<PendingIdentity>> GetForBusinessAsync(Guid queryBusinessId) =>
            _repository.FindAsync(p => p.BusinessId == queryBusinessId);

        public Task<PendingIdentity> GetAsync(Guid id, Guid businessId) =>
            _repository.GetAsync(p => p.Id == id && p.BusinessId == businessId);

        public Task RemoveAsync(PendingIdentity pending) => _repository.RemoveAsync(pending.Id);
    }
}
