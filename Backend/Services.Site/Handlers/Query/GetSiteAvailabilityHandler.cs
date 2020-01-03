using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetSiteAvailabilityHandler : IQueryHandler<GetSiteAvailability, SiteAvailabilityDto>
    {
        private readonly IVmsLogger<GetSiteAvailabilityHandler> _logger;
        private readonly ISiteRepository _siteRepository;
        private readonly ISystemServices _systemServices;

        public GetSiteAvailabilityHandler(IVmsLogger<GetSiteAvailabilityHandler> logger, ISiteRepository siteRepository, ISystemServices systemServices)
        {
            _logger = logger;
            _siteRepository = siteRepository;
            _systemServices = systemServices;
        }


        public async Task<SiteAvailabilityDto> HandleAsync(GetSiteAvailability query)
        {
            var site = await _siteRepository.GetAsync(query.SiteId);
            if (site is null)
            {
                throw new VmsException(Codes.InvalidId, $"The site with a id {query.SiteId} could not be found.");
            }

            var siteDto = new SiteAvailabilityDto {SiteName = site.Name};
            var records = await _systemServices.GetLatestAccessRecordsForSite(query.SiteId);
            var userRecords = new List<SiteUserDto>();
            foreach (var record in records)
            {
                userRecords.Add(new SiteUserDto()
                {
                    ContactNumber = record.ContactNumber,
                    Email = record.Email,
                    Id = record.Id,
                    Name = record.FullName,
                    Status = record.Action,
                    Time = record.TimeStamp.ToShortTimeString(),
                    Date = record.TimeStamp.ToShortDateString()
                });
            }

            siteDto.Users = userRecords;

            return siteDto;
        }
    }
}
