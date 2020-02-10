using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Services.Common.Jwt;
using Services.Common.Mongo;

namespace Services.Identity.Repositorys
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IMongoRepository<Domain.Identity> _repository;

        public IdentityRepository(IMongoRepository<Domain.Identity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsEmailInUse(string email, string role) =>
            await _repository.GetAsync(i => i.Email == email && i.Role == role) != null;

        public Task<Domain.Identity> GetByEmailAndRole(string email, string role) =>
            _repository.GetAsync(i => i.Email == email && i.Role == role);

        public Task AddAsync(Domain.Identity identity) =>
            _repository.AddAsync(identity);

        public Task<IEnumerable<Domain.Identity>> GetAdminsForBusinessAsync(Guid businessId) =>
            _repository.FindAsync(p => p.BusinessId == businessId && p.Role == Roles.BusinessAdmin);

        public Task<Domain.Identity> GetAsync(Guid id, Guid businessId) =>
            _repository.GetAsync(i => i.Id == id && i.BusinessId == businessId);

        public Task RemoveAsync(Domain.Identity identity)
            => _repository.RemoveAsync(identity.Id);

        public Task<Domain.Identity> GetByEmail(string email)
            => _repository.GetAsync(i => i.Email == email);

        public Task UpdateAsync(Domain.Identity identity) => _repository.UpdateAsync(identity, identity.Id);

        public Task<IEnumerable<Domain.Identity>> GetStandardAccountsForBusinessAsync(Guid businessId) => _repository.FindAsync(i => i.Role == Roles.StandardPortalUser && i.BusinessId == businessId);

        public Task<Domain.Identity> GetStandardAccountsForBusinessAsync(Guid businessId, Guid accountId)
        => _repository.GetAsync(i => i.Role == Roles.StandardPortalUser && i.BusinessId == businessId && i.Id == accountId);
    }
}
