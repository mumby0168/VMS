using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Sites.Dtos;
using Services.Sites.Messages.Queries;
using Services.Sites.Repositorys;

namespace Services.Sites.Handlers.Query
{
    public class GetSiteSummariesHandler : IQueryHandler<GetSiteSummaries, IEnumerable<SiteSummaryDto>>
    {
        private readonly IVmsLogger<GetSiteSummariesHandler> _logger;
        private readonly ISiteRepository _repository;

        public GetSiteSummariesHandler(IVmsLogger<GetSiteSummariesHandler> logger, ISiteRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IEnumerable<SiteSummaryDto>> HandleAsync(GetSiteSummaries query)
        {
            var sites = await _repository.GetSitesForBusinessAsync(query.BusinessId);

            var siteDtos = new List<SiteSummaryDto>();

            foreach (var site in sites)
            {
                siteDtos.Add(new SiteSummaryDto{Id = site.Id, Name = site.Name});
            }

            return siteDtos;
        }
    }
}
