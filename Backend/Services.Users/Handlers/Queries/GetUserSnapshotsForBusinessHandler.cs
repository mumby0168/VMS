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

        public GetUserSnapshotsForBusinessHandler(IVmsLogger<GetUserSnapshotsForBusinessHandler> logger, IUserRepository userRepository, IAccessRecordRepository accessRecordRepository, ISiteRepository siteRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _accessRecordRepository = accessRecordRepository;
            _siteRepository = siteRepository;
        }


        public async Task<IEnumerable<UserSnapshotDto>> HandleAsync(GetUserSnapshotsForBusiness query)
        {
            var users = await _userRepository.GetUsersByBusinessId(query.BusinessId);

            var ret = new List<UserSnapshotDto>();

            foreach (var user in users)
            {
                var records = await _accessRecordRepository.GetForUser(user.Id);
                var latest = records.OrderByDescending(r => r.TimeStamp).FirstOrDefault();

                if(latest is null) continue;

                var siteName = await _siteRepository.GetSiteNameAsync(latest.SiteId);

                ret.Add(new UserSnapshotDto
                {
                    Id = user.Id, 
                    Initials = $"{user.FirstName[0]}{user.SecondName[0]}",
                    LastAction = latest.Action.ToString(),
                    LastTime = latest.TimeStamp.ToShortTimeString(),
                    Name = user.FirstName + " " + user.SecondName,
                    SiteName = siteName
                });

            }

            return ret.OrderBy(r => r.Name);
        }
    }
}
