using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Queries;
using Services.Sites.Dtos;
using Services.Sites.Messages.Queries;

namespace Services.Sites.Handlers.Query
{
    public class GetSiteResourcesHandler  : IQueryHandler<GetSiteResources, IEnumerable<SiteResourceDto>>
    {
        private readonly ILogger<GetSiteResourcesHandler> _logger;

        public GetSiteResourcesHandler(ILogger<GetSiteResourcesHandler> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<SiteResourceDto>> HandleAsync(GetSiteResources query)
        {
            throw new NotImplementedException();
        }
    }
}
