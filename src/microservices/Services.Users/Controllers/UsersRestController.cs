using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Mongo;
using Services.Common.Rest;
using Services.Users.Domain;

namespace Services.Users.Controllers
{
    public class UserDto
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        public string Email { get; set; }
        
        public Guid BasedSiteId { get; set; }
        
        public Guid BusinessId { get;  set; }
        
        public Guid AccountId { get; set; }
        
    }


    [Route("users/api/rest/")]
    public class UsersRestController : RestControllerBase<User, UserDto>
    {
        public UsersRestController(IMongoRepository<User> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}