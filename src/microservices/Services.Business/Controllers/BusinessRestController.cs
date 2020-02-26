using AutoMapper;
using Services.Business.Dtos;
using Services.Common.Mongo;
using Services.Common.Rest;

namespace Services.Business.Controllers
{
    public class BusinessRestController : RestControllerBase<Domain.BusinessDocument, BusinessDto>
    {
        public BusinessRestController(IMongoRepository<Domain.BusinessDocument> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}