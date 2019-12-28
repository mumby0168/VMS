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

namespace Services.Users.Handlers.Queries
{
    public class GetPersonalAccessRecordsHandler : IQueryHandler<GetPersonalAccessRecords, IEnumerable<AccessRecordDto>>
    {
        private readonly IVmsLogger<GetPersonalAccessRecordsHandler> _logger;
        private readonly IAccessRecordRepository _accessRecordRepository;

        public GetPersonalAccessRecordsHandler(IVmsLogger<GetPersonalAccessRecordsHandler> logger, IAccessRecordRepository accessRecordRepository)
        {
            _logger = logger;
            _accessRecordRepository = accessRecordRepository;
        }

        public async Task<IEnumerable<AccessRecordDto>> HandleAsync(GetPersonalAccessRecords query)
        {
            var records = await _accessRecordRepository.GetForUser(query.UserId);

            var ret = new List<AccessRecordDto>();
            //TODO: Get site name somehow. (cross service | hold internal)
            foreach (var accessRecord in records)
            {
                ret.Add(new AccessRecordDto
                {
                    Action = accessRecord.Action == AccessAction.In ? "in" : "out",
                    Id = accessRecord.Id,
                    SiteId = accessRecord.SiteId,
                    TimeStamp = accessRecord.TimeStamp
                });
            }

            return ret;
        }
    }
}
