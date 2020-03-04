using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Libmongocrypt;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Visitors.Dtos;
using Services.Visitors.Queries;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Queries
{
    public class GetVisitorsOnSiteHandler : IQueryHandler<GetVisitorsOnSite, IEnumerable<VisitorDto>>
    {
        private readonly IVmsLogger<GetVisitorsOnSiteHandler> _logger;
        private readonly IVisitorsRepository _visitorsRepository;
        private readonly ISpecificationRepository _specificationRepository;

        public GetVisitorsOnSiteHandler(IVmsLogger<GetVisitorsOnSiteHandler> logger, IVisitorsRepository visitorsRepository, ISpecificationRepository specificationRepository)
        {
            _logger = logger;
            _visitorsRepository = visitorsRepository;
            _specificationRepository = specificationRepository;
        }
        
        public async Task<IEnumerable<VisitorDto>> HandleAsync(GetVisitorsOnSite query)
        {
            var visitors = await _visitorsRepository.GetForSiteAsync(query.SiteId);
            

            var visitorDocuments = visitors.ToList();
            
            if (!visitorDocuments.Any())
                return null;


            Guid nameSpecId =
                await _specificationRepository.GetNameSpecIdForBusinessAsync(
                    visitorDocuments.First().VisitingBusinessId);
            
            return visitorDocuments.Select(v => new VisitorDto
            {
                Id = v.Id,
                Name = v.Data.First(spec => spec.DataSpecificationId == nameSpecId).Value
            });
        }
    }
}