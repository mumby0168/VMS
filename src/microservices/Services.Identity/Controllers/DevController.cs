using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Common.Mongo;
using Services.Identity.Domain;

namespace Services.Identity.Controllers
{

    public class CodeResult
    {
        public Guid Code { get; set; }

        public string Email { get; set; }
    }

    [Route("api/dev/")]
    public class DevController : VmsControllerBase
    {
        private readonly IMongoRepository<PendingIdentity> _pendingRepository;

        public DevController(IMongoRepository<PendingIdentity> pendingRepository)
        {
            _pendingRepository = pendingRepository;
        }

        [HttpGet("accountCodes")]
        public async Task<ActionResult<IEnumerable<CodeResult>>> GetPendingAccountCode()
        {
            var pending = await _pendingRepository.GetAllAsync();

            var ret = new List<CodeResult>();

            foreach (var account in pending)
            {
                ret.Add(new CodeResult()
                {
                    Code = account.Id,
                    Email = account.Email
                });
            }

            return Ok(ret);
        }
    }
}