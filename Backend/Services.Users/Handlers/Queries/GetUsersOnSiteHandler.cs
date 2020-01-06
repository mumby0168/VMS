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
    public class GetUsersOnSiteHandler : IQueryHandler<GetUsersOnSite, IEnumerable<OnSiteAccessRecordDto>>
    {
        private readonly IVmsLogger<GetUsersOnSiteHandler> _logger;
        private readonly IAccessRecordRepository _repository;
        private readonly IUserRepository _usersRepository;

        public GetUsersOnSiteHandler(IVmsLogger<GetUsersOnSiteHandler> logger, IAccessRecordRepository repository, IUserRepository usersRepository)
        {
            _logger = logger;
            _repository = repository;
            _usersRepository = usersRepository;
        }

        public async Task<IEnumerable<OnSiteAccessRecordDto>> HandleAsync(GetUsersOnSite query)
        {
            var records = await _repository.GetForSite(query.SiteId);
            var userAccessRecords = records.GroupBy(a => a.UserId);

            var ret = new List<OnSiteAccessRecordDto>();

            foreach (var userAccessRecord in userAccessRecords)
            {
                var latest = userAccessRecord.OrderByDescending(a => a.TimeStamp).FirstOrDefault();

                //TODO: don't be lazy.
                if (latest is null)
                    continue;

                var user = await _usersRepository.GetAsync(userAccessRecord.Key);

                if (latest.Action == AccessAction.In)
                {
                    ret.Add(new OnSiteAccessRecordDto
                    {
                        Id = latest.Id,
                        UserId = latest.UserId,
                        TimeStamp = latest.TimeStamp,
                        FullName = user.FirstName + " " + user.SecondName
                    });
                }
            }

            return ret.OrderByDescending(a => a.TimeStamp);
        }
    }
}
