using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Identity.Domain;

namespace Services.Identity.Repositorys
{
    public class ResetRequestRepository : IResetRequestRepository
    {
        private readonly IMongoRepository<ResetRequest> _repository;

        public ResetRequestRepository(IMongoRepository<ResetRequest> repository)
        {
            _repository = repository;
        }
        public Task AddAsync(IResetRequest request) => _repository.AddAsync(request as ResetRequest);

        public async Task<IResetRequest> GetAsync(Guid code) => await _repository.GetAsync(code);
    }
}
