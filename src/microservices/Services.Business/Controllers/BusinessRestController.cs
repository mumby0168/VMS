using AutoMapper;
using Services.Business.Dtos;
using Services.Common.Mongo;
using Services.Common.Rest;

namespace Services.Business.Controllers
{
    public class BusinessRestController : RestControllerBase<Domain.Business, BusinessDto>
    {
        public BusinessRestController(IMongoRepository<Domain.Business> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}