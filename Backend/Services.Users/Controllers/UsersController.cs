using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Mongo;
using Services.Users.Domain;

namespace Services.Users.Controllers
{
    [Route("users/api/")]
    public class UsersController : Controller
    {
        private readonly IMongoRepository<User> _repository;

        public UsersController(IMongoRepository<User> repository)
        {
            _repository = repository;
        }
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<IUser>>> Get()
        {
            return Ok(await _repository.GetAllAsync());
        }


    }
}