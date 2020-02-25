using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Mongo;
using Services.Common.Rest;
using Services.Users.Domain;

namespace Services.Users.Controllers
{
    public class UserDto
    {

    }


    [Route("users/api/rest/")]
    public class UsersRestController : RestControllerBase<User, UserDto>
    {
        public UsersRestController(IMongoRepository<User> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}