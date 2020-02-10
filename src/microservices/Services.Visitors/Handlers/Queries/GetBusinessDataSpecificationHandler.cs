using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Visitors.Dtos;
using Services.Visitors.Queries;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Queries
{
    public class GetBusinessDataSpecificationHandler : IQueryHandler<GetBusinessDataSpecifications, IEnumerable<DataSpecificationDto>>
    {
        private readonly IVmsLogger<GetBusinessDataSpecificationHandler> _logger;
        private readonly IDataSpecificationRepository _repository;

        public GetBusinessDataSpecificationHandler(IVmsLogger<GetBusinessDataSpecificationHandler> logger, IDataSpecificationRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<IEnumerable<DataSpecificationDto>> HandleAsync(GetBusinessDataSpecifications query)
        {
            var specifications = await _repository.GetEntriesAsync(query.BusinessId);
            var ret = new List<DataSpecificationDto>();

            foreach (var dataSpecification in specifications)
            {
                ret.Add(new DataSpecificationDto
                {
                    Id = dataSpecification.Id,
                    Order = dataSpecification.Order,
                    ValidationCode = dataSpecification.ValidationCode,
                    ValidationMessage = dataSpecification.ValidationMessage,
                    Label = dataSpecification.Label
                });
            }

            return ret.OrderBy(d => d.Order);
        }
    }
}
