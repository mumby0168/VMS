using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;
using Services.Visitors.Dtos;
using Services.Visitors.Queries;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Queries
{
    public class VisitorRecordsForBusinessHandler : IQueryHandler<VisitorRecordsForBusiness, IEnumerable<VisitorRecordDto>>
    {
        private readonly IVisitorsRepository _visitorsRepository;
        private readonly ISiteServiceClient _siteServiceClient;
        private readonly ISpecificationRepository _specificationRepository;

        public VisitorRecordsForBusinessHandler(IVisitorsRepository visitorsRepository, ISiteServiceClient siteServiceClient
        , ISpecificationRepository specificationRepository)
        {
            _visitorsRepository = visitorsRepository;
            _siteServiceClient = siteServiceClient;
            _specificationRepository = specificationRepository;
        }
        public async Task<IEnumerable<VisitorRecordDto>> HandleAsync(VisitorRecordsForBusiness query)
        {
            var visitors = await _visitorsRepository.GetVistorsForBusinessAsync(query.BusinessId);

            var sites = await _siteServiceClient.GetSitesForBusinessAsync(query.BusinessId);

            var nameSpecification = await _specificationRepository.GetNameSpecIdForBusinessAsync(query.BusinessId);

            return visitors.Select(v => new VisitorRecordDto
            {
                Id = v.Id,
                InTime = v.In.ToShortTimeString(),
                OutTime = v.Out?.ToShortTimeString() ?? string.Empty,
                Date = v.In.ToShortDateString(),
                SiteId = v.VisitingSiteId,
                SiteName = sites.FirstOrDefault(s => s.Id == v.VisitingSiteId)?.Name,
                Name = v.Data.FirstOrDefault(d => d.DataSpecificationId == nameSpecification)?.Value
            });
        }
    }
}