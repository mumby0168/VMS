using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Common.Mongo;
using Services.Common.Queries;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Users.Domain;
using Services.Users.Dtos;
using Services.Users.Queries;

namespace Services.Users.Controllers
{
    [Route("users/api/")]
    public class UsersController : VmsControllerBase
    {
        private readonly IMongoRepository<User> _repository;
        private readonly IQueryDispatcher _dispatcher;

        public UsersController(IMongoRepository<User> repository, IQueryDispatcher dispatcher)
        {
            _repository = repository;
            _dispatcher = dispatcher;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<IUser>>> Get()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("info/{accountId}")]
        public async Task<ActionResult<UserInfoDto>> GetUserInfo(Guid accountId)
        {
            return Single(await _dispatcher.Dispatch<GetUserInfo, UserInfoDto>(new GetUserInfo(accountId)));
        }

        [HttpGet("admin-records/{userId}")]
        public async Task<ActionResult<IEnumerable<AccessRecordDto>>> Get([FromRoute]Guid userId)
        {
            return Collection(await _dispatcher.Dispatch<GetPersonalAccessRecords, IEnumerable<AccessRecordDto>>(
                new GetPersonalAccessRecords(Guid.Empty, userId)));
        }

        [HttpGet("users/{businessId}")]
        public async Task<ActionResult<IEnumerable<UserSnapshotDto>>> GetUsersForBusiness(Guid businessId)
        {
            return Collection(
                await _dispatcher.Dispatch<GetUserSnapshotsForBusiness, IEnumerable<UserSnapshotDto>>(
                    new GetUserSnapshotsForBusiness(businessId)));
        }


    }
}