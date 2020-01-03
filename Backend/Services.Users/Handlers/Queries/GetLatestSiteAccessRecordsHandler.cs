using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Users.Domain;
using Services.Users.Dtos;
using Services.Users.Factories;
using Services.Users.Queries;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Queries
{
    public class GetLatestSiteAccessRecordsHandler : IQueryHandler<GetLatestSiteAccessRecords, IEnumerable<LatestAccessRecordDto>>
    {
        private readonly IVmsLogger<GetBusinessAccessRecordsHandler> _logger;
        private readonly IAccessRecordRepository _accessRecordRepository;
        private readonly IUserRepository _userRepository;

        public GetLatestSiteAccessRecordsHandler(IVmsLogger<GetBusinessAccessRecordsHandler> logger, IAccessRecordRepository accessRecordRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _accessRecordRepository = accessRecordRepository;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<LatestAccessRecordDto>> HandleAsync(GetLatestSiteAccessRecords query)
        {
            var records = new List<LatestAccessRecordDto>();

            var accessRecords = await _accessRecordRepository.GetForSite(query.SiteId);
            var userAccessRecords = accessRecords.GroupBy(a => a.UserId);

            foreach (var userAccessRecord in userAccessRecords)
            {
                var user = await _userRepository.GetAsync(userAccessRecord.Key);

                var recordDto = new LatestAccessRecordDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    ContactNumber = user.PhoneNumber,
                    FullName = user.FirstName + " " + user.SecondName
                };

                var latest = userAccessRecord.OrderByDescending(a => a.TimeStamp).FirstOrDefault();

                //TODO: don't be lazy.
                if (latest is null)
                    continue;

                recordDto.Action = latest.Action == AccessAction.In ? "in" : "out";
                recordDto.Id = latest.Id;
                recordDto.TimeStamp = latest.TimeStamp;
                records.Add(recordDto);
            }

            return records;
        }
    }
}
