using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polly.Retry;
using Services.Common.Queries;
using Services.Visitors.Dtos;
using Services.Visitors.Queries;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Queries
{
    public class VisitorInformationHandler : IQueryHandler<VisitorInformation,IEnumerable<VisitorInformationDto>>
    {
        private readonly IVisitorsRepository _visitorsRepository;
        private readonly ISpecificationRepository _specificationRepository;

        public VisitorInformationHandler(IVisitorsRepository visitorsRepository, ISpecificationRepository specificationRepository)
        {
            _visitorsRepository = visitorsRepository;
            _specificationRepository = specificationRepository;
        }
        public async Task<IEnumerable<VisitorInformationDto>> HandleAsync(VisitorInformation query)
        {
            var visitor = await _visitorsRepository.GetAsync(query.VisitorId);
            if (visitor is null)
            {
                return null;
            }

            var data = await _specificationRepository.GetEntriesForBusinessAsync(visitor.VisitingBusinessId);

            return visitor.Data.Select(visitorData =>
            {
                var spec = data.FirstOrDefault(d => d.Id == visitorData.DataSpecificationId);
                return new VisitorInformationDto
                {
                    Value = visitorData.Value,
                    SpecificationId = visitorData.DataSpecificationId,
                    Label = spec?.Label,
                };
            });
        }
    }
}