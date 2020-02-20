using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Users.Dtos;
using Services.Users.Queries;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Queries
{
    public class GetUserSnapshotsForBusinessHandler : IQueryHandler<GetUserSnapshotsForBusiness, IEnumerable<UserSnapshotDto>>
    {
        private readonly IVmsLogger<GetUserSnapshotsForBusinessHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAccessRecordRepository _accessRecordRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IUserStatusRepository _statusRepository;

        public GetUserSnapshotsForBusinessHandler(IVmsLogger<GetUserSnapshotsForBusinessHandler> logger, IUserRepository userRepository, IAccessRecordRepository accessRecordRepository, ISiteRepository siteRepository, IUserStatusRepository statusRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _accessRecordRepository = accessRecordRepository;
            _siteRepository = siteRepository;
            _statusRepository = statusRepository;
        }


        public async Task<IEnumerable<UserSnapshotDto>> HandleAsync(GetUserSnapshotsForBusiness query)
        {
            var users = await _userRepository.GetUsersByBusinessId(query.BusinessId);

            var ret = new List<UserSnapshotDto>();

            foreach (var user in users)
            {
                //TODO: This could get these all in one go then iterate the list in memory. (FirstOrDefault())
                var state = await _statusRepository.GetStatusForUserAsync(user.Id);                

                //TODO: Could get list of sites and access then from memory.
                var siteName = await _siteRepository.GetSiteNameAsync(state.SiteId);

                ret.Add(new UserSnapshotDto
                {
                    Id = user.Id, 
                    Initials = $"{user.FirstName[0]}{user.SecondName[0]}",
                    LastAction = state.CurrentState.ToString(),
                    LastTime = state.Updated.ToShortTimeString(),
                    Name = user.FirstName + " " + user.SecondName,
                    SiteName = siteName,
                    AccountId = user.AccountId
                });

            }

            return ret.OrderBy(r => r.Name);
        }
    }
}
