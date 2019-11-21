using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;
using Services.Identity.Dtos;
using Services.Identity.Messages.Query;
using Services.Identity.Repositorys;

namespace Services.Identity.Handlers.Query
{
    public class GetAllBusinessAdminsHandler : IQueryHandler<GetAllBusinessAdmins, IEnumerable<AccountInfoDto>>
    {
        private readonly IPendingIdentityRepository _pendingRepository;
        private readonly IIdentityRepository _identityRepository;

        public GetAllBusinessAdminsHandler(IPendingIdentityRepository pendingRepository, IIdentityRepository identityRepository)
        {
            _pendingRepository = pendingRepository;
            _identityRepository = identityRepository;
        }
        public async Task<IEnumerable<AccountInfoDto>> HandleAsync(GetAllBusinessAdmins query)
        {
            var accounts = new List<AccountInfoDto>();

            var pending = await _pendingRepository.GetForBusinessAsync(query.BusinessId);
            var identities = await _identityRepository.GetForBusinessAsync(query.BusinessId);

            foreach (var pendingIdentity in pending)
                accounts.Add(new AccountInfoDto{Email = pendingIdentity.Email, Id = pendingIdentity.Id, IsPending = true});

            foreach (var identity in identities)
                accounts.Add(new AccountInfoDto { Email = identity.Email, Id = identity.Id, IsPending = false, CreatedOn = identity.CreatedOn});

            return accounts;
        }
    }
}
