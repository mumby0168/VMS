using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Queries;
using Services.Sites.Domain;
using Services.Sites.Dtos;
using Services.Sites.Messages.Queries;
using Services.Sites.Repositorys;

namespace Services.Sites.Handlers.Query
{
    public class GetSiteResourcesHandler  : IQueryHandler<GetSiteResources, IEnumerable<SiteResourceDto>>
    {
        private readonly ILogger<GetSiteResourcesHandler> _logger;
        private readonly ISiteResourceRepository _repository;

        public GetSiteResourcesHandler(ILogger<GetSiteResourcesHandler> logger, ISiteResourceRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IEnumerable<SiteResourceDto>> HandleAsync(GetSiteResources query)
        {
            var resources = await _repository.GetSiteResources(query.SiteId);

            var ret = new List<SiteResourceDto>();

            foreach (var resource in resources)
            {
                ret.Add(new SiteResourceDto
                {
                    Id = resource.Id,
                    Identifier = resource.Identifier,
                    Name =  resource.Name
                });
            }

            return ret;
        }
    }
}
