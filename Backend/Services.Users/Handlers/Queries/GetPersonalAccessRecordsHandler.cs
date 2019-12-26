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
    public class GetPersonalAccessRecordsHandler : IQueryHandler<GetPersonalAccessRecords, IEnumerable<AccessRecordDto>>
    {
        private readonly IVmsLogger<GetPersonalAccessRecordsHandler> _logger;
        private readonly IAccessRecordRepository _accessRecordRepository;

        public GetPersonalAccessRecordsHandler(IVmsLogger<GetPersonalAccessRecordsHandler> logger, IAccessRecordRepository accessRecordRepository)
        {
            _logger = logger;
            _accessRecordRepository = accessRecordRepository;
        }

        public Task<IEnumerable<AccessRecordDto>> HandleAsync(GetPersonalAccessRecords query)
        {
            
        }
    }
}
