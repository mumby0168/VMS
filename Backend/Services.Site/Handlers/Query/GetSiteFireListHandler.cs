using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Sites.Dtos;
using Services.Sites.Messages.Queries;
using Services.Sites.Repositorys;
using Services.Sites.Services;

namespace Services.Sites.Handlers.Query
{
    public class GetSiteFireListHandler : IQueryHandler<GetSiteFireList, SiteFireListDto>
    {
        private readonly IVmsLogger<GetSiteFireListHandler> _logger;
        private readonly ISystemServices _systemServices;
        private readonly ISiteRepository _siteRepository;

        public GetSiteFireListHandler(IVmsLogger<GetSiteFireListHandler> logger, ISystemServices systemServices, ISiteRepository siteRepository)
        {
            _logger = logger;
            _systemServices = systemServices;
            _siteRepository = siteRepository;
        }

        public async Task<SiteFireListDto> HandleAsync(GetSiteFireList query)
        {
            var site = await _siteRepository.GetAsync(query.SiteId);
            if(site is null) throw new VmsException(Codes.InvalidId, $"The site with the id {query.SiteId} could not be found");

            var fireList = new SiteFireListDto()
            {
                SiteId = site.Id,
                Time = DateTime.Now.ToShortTimeString(),
                Date = DateTime.Now.ToShortDateString()
            };

            var onSite = await _systemServices.GetUsersOnSiteAsync(query.SiteId);

            var users = new List<FireListUserDto>();

            foreach (var onSiteAccessRecordDto in onSite)
            {
                users.Add(new FireListUserDto
                {
                    Id = onSiteAccessRecordDto.Id,
                    Time = onSiteAccessRecordDto.TimeStamp.ToShortTimeString(),
                    Date = onSiteAccessRecordDto.TimeStamp.ToShortDateString(),
                    FullName = onSiteAccessRecordDto.FullName
                });
            }

            fireList.Employees = users;

            return fireList;
        }
    }
}
;