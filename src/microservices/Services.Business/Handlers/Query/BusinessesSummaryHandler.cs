using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Business.Dtos;
using Services.Business.Messages.Queries;
using Services.Business.Repositorys;
using Services.Common.Logging;
using Services.Common.Queries;

namespace Services.Business.Handlers.Query
{
    public class BusinessesSummaryHandler : IQueryHandler<BusinessesSummary, IEnumerable<BusinessSummaryDto>>
    {
        private readonly IVmsLogger<BusinessesSummaryHandler> _logger;
        private readonly IBusinessRepository _repository;

        public BusinessesSummaryHandler(IVmsLogger<BusinessesSummaryHandler> logger, IBusinessRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }


        public async Task<IEnumerable<BusinessSummaryDto>> HandleAsync(BusinessesSummary query)
        {
            var businesses = await _repository.GetBusinessesAsync();
            var returns = new List<BusinessSummaryDto>();
            foreach (var business in businesses)
            {
                returns.Add(new BusinessSummaryDto()
                    { Id = business.Id, Name = business.Name, TradingName = business.TradingName });
            }

            _logger.LogInformation($"{returns.Count} summaries returned.");

            return returns;
        }
    }
}
