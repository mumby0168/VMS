using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Users.Domain;
using Services.Users.Dtos;
using Services.Users.Queries;
using Services.Users.Repositories;
using Services.Users.Services;

namespace Services.Users.Handlers.Queries
{
    public class GetBusinessAccessRecordsHandler : IQueryHandler<GetBusinessAccessRecords, IEnumerable<SiteAccessDetailsDto>>
    {
        private readonly IVmsLogger<GetBusinessAccessRecordsHandler> _logger;
        private readonly IAccessRecordRepository _accessRecordRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISiteRepository _siteRepository;

        public GetBusinessAccessRecordsHandler(IVmsLogger<GetBusinessAccessRecordsHandler> logger, IAccessRecordRepository accessRecordRepository, IUserRepository userRepository, ISiteRepository siteRepository)
        {
            _logger = logger;
            _accessRecordRepository = accessRecordRepository;
            _userRepository = userRepository;
            _siteRepository = siteRepository;
        }
        public async Task<IEnumerable<SiteAccessDetailsDto>> HandleAsync(GetBusinessAccessRecords query)
        {
            var records = await _accessRecordRepository.GetForBusiness(query.BusinessId);

            var siteRecords = new List<SiteAccessDetailsDto>();

            var siteGroups = records.GroupBy(r => r.SiteId);
            foreach (var siteGroup in siteGroups)
            {
                var siteRecord = new SiteAccessDetailsDto();
                var siteName = await _siteRepository.GetSiteNameAsync(siteGroup.Key);
                siteRecord.SiteName = siteName;
                var userRecords = new List<UserAccessDetailsDto>();
                var userGroups = siteGroup.GroupBy(s => s.UserId);

                foreach (var userGroup in userGroups)
                {
                    var userRecord = new UserAccessDetailsDto();
                    var user = await _userRepository.GetAsync(userGroup.Key);
                    var accessRecords = new List<AccessRecordDto>();

                    foreach (var accessRecord in userGroup)
                    {
                        accessRecords.Add(new AccessRecordDto()
                        {
                            Action = accessRecord.Action == AccessAction.In ? "in" : "out",
                            Id = accessRecord.Id,
                            TimeStamp = accessRecord.TimeStamp
                        });
                    }

                    userRecord.Records = accessRecords;
                    userRecord.Name = user.FirstName + " " + user.SecondName;
                    userRecord.Id = user.Id;
                    userRecords.Add(userRecord);
                }

                siteRecord.UserRecords = userRecords;
                siteRecords.Add(siteRecord);
            }

            return siteRecords;
        }
    }
}
