using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Users.Domain;
using Services.Users.Dtos;
using Services.Users.Queries;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Queries
{
    public class GetPersonalAccessRecordsHandler : IQueryHandler<GetPersonalAccessRecords, IEnumerable<AccessRecordDto>>
    {
        private readonly IVmsLogger<GetPersonalAccessRecordsHandler> _logger;
        private readonly IAccessRecordRepository _accessRecordRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISiteRepository _siteRepository;

        public GetPersonalAccessRecordsHandler(IVmsLogger<GetPersonalAccessRecordsHandler> logger, IAccessRecordRepository accessRecordRepository, IUserRepository userRepository, ISiteRepository siteRepository)
        {
            _logger = logger;
            _accessRecordRepository = accessRecordRepository;
            _userRepository = userRepository;
            _siteRepository = siteRepository;
        }

        public async Task<IEnumerable<AccessRecordDto>> HandleAsync(GetPersonalAccessRecords query)
        {
            var user = query.AccountId == Guid.Empty ? 
                await _userRepository.GetAsync(query.UserId) : 
                await _userRepository.GetFromAccountId(query.AccountId);


            if (user is null)
            {
                _logger.LogError($"User not found with id: {query.AccountId}");
                throw new VmsException(Codes.InvalidId, "The user with that id requested cannot be found.");
            }


            var records = await _accessRecordRepository.GetForUser(user.Id);

            var ret = new List<AccessRecordDto>();
            foreach (var accessRecord in records)
            {

                var site = await _siteRepository.GetSiteNameAsync(accessRecord.SiteId);

                ret.Add(new AccessRecordDto
                {
                    Action = accessRecord.Action == AccessAction.In ? "in" : "out",
                    Id = accessRecord.Id,
                    SiteId = accessRecord.SiteId,
                    Date = accessRecord.TimeStamp.ToShortDateString(),
                    Time = accessRecord.TimeStamp.ToShortTimeString(),
                    TimeStamp = accessRecord.TimeStamp,
                    SiteName = site
                });
            }

            return ret.OrderByDescending(r => r.TimeStamp);
        }
    }
}
