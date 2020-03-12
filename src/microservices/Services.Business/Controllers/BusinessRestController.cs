using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Business.Dtos;
using Services.Common.Mongo;
using Services.Common.Rest;

namespace Services.Business.Controllers
{
    [Route("business/api/rest")]
    public class BusinessRestController : RestControllerBase<Domain.BusinessDocument, BusinessSummaryDto>
    {
        public BusinessRestController(IMongoRepository<Domain.BusinessDocument> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}