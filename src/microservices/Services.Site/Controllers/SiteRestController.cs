using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Mongo;
using Services.Common.Rest;
using Services.Sites.Domain;
using Services.Sites.Dtos;

namespace Services.Sites.Controllers
{
    [Route(("site/api/rest/"))]
    public class SiteRestController : RestControllerBase<Site, SiteDto>
    {
        public SiteRestController(IMongoRepository<Site> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}