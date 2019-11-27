using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Queries;
using Services.Sites.Dtos;
using Services.Sites.Messages.Queries;
using Services.Sites.Repositorys;

namespace Services.Sites.Handlers.Query
{
    public class GetSiteHandler : IQueryHandler<GetSite, SiteDto>
    {
        private readonly ILogger<GetSiteHandler> _logger;
        private readonly ISiteRepository _repository;

        public GetSiteHandler(ILogger<GetSiteHandler> logger, ISiteRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<SiteDto> HandleAsync(GetSite query)
        {
            var site = await _repository.GetAsync(query.Id);

            if (site is null) return null;

            return new SiteDto
            {
                BusinessId = query.Id,
                Id = site.Id,
                Name = site.Name,
                AddressLine1 = site.AddressLine1,
                AddressLine2 = site.AddressLine2,
                PostCode = site.PostCode,
                ContactNumber = site.GetContact().ContactNumber,
                FirstName = site.GetContact().FirstName,
                SecondName = site.GetContact().SecondName,
                Email = site.GetContact().Email
            };
        }
    }
}
