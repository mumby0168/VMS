using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Services.Common.Base;
using Services.Common.Jwt;
using Services.Common.Queries;
using Services.Identity.Dtos;
using Services.Identity.Messages.Commands;
using Services.Identity.Messages.Query;
using Services.Identity.Services;

namespace Services.Identity.Controllers
{
    [Route("api/admin/")]
    public class AdminController : VmsControllerBase
    {
        private readonly IAdminIdentityService _adminIdentityService;
        private readonly IQueryDispatcher _queryDispatcher;

        public AdminController(IAdminIdentityService adminIdentityService, IQueryDispatcher queryDispatcher)
        {
            _adminIdentityService = adminIdentityService;
            _queryDispatcher = queryDispatcher;
        }

        [AllowAnonymous]
        [HttpPost("sign-in-system")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command) =>
            Ok(await _adminIdentityService.SignIn(command.Email, command.Password, Roles.SystemAdmin));

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAdmin command)
        {
            await _adminIdentityService.CreateAdmin(command.Email);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteAdmin command)
        {
            await _adminIdentityService.CompleteAdmin(command.Code, command.Password, command.PasswordMatch,
                command.Email);
            return Ok();
        }

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpPost("create-business-admin")]
        public async Task<IActionResult> Create([FromBody] CreateBusinessAdmin command)
        {
            await _adminIdentityService.CreateBusinessAdmin(command.Email, command.BusinessId);
            return Ok();
        }

        [Authorize(Roles = Roles.SystemAdmin)]
        [HttpGet("get-for-business/{businessId}")]
        public async Task<ActionResult<IEnumerable<AccountInfoDto>>> Get([FromRoute] Guid businessId)
        {
            return Collection(
                await _queryDispatcher.Dispatch<GetAllBusinessAdmins, IEnumerable<AccountInfoDto>>(
                    new GetAllBusinessAdmins(businessId)));
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Delete([FromBody]DeleteBusinessAdmin command)
        {
            await _adminIdentityService.DeleteBusinessAdmin(command.Id, command.BusinessId);
            return Ok();
        }

    }
}
