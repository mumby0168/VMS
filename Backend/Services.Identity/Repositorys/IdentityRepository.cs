using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
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

        public async Task<bool> IsEmailInUse(string email) => 
            await _repository.GetAsync(i => i.Email == email) != null;

        public Task<Domain.Identity> GetByEmailAndRole(string email, string role) =>
            _repository.GetAsync(i => i.Email == email && i.Role == role);

        public Task AddAsync(Domain.Identity identity) => 
            _repository.AddAsync(identity);
    }
}
