using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Logs.Domain;
using Services.Logs.Repositorys;

namespace Services.Logs.Controllers
{
    [Route("api/logs/")]
    public class LogsController : VmsControllerBase
    {
        private readonly ILogsRepository _logsRepository;

        public LogsController(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ILog>>> GetAll()
        {
            return Collection(await _logsRepository.GetAllAsync());
        }

        [HttpPost("purge")]
        public Task Purge()
        {
            return _logsRepository.Purge();
        }

    }
}
