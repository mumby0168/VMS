using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Base;
using Services.Logs.Decode;
using Services.Logs.Domain;
using Services.Logs.Dtos;
using Services.Logs.Repositorys;

namespace Services.Logs.Controllers
{
    [EnableCors]
    [Route("api/logs/")]
    public class LogsController : VmsControllerBase
    {
        private readonly ILogsRepository _logsRepository;

        public LogsController(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogDto>>> GetAll()
        {
            var logs = await _logsRepository.GetAllAsync();
            var dtos = new List<LogDto>();
            foreach (var log in logs.OrderByDescending(o => o.Created))
            {
                dtos.Add(new LogDto
                {
                    Id = log.Id,
                    Time = log.Created.ToShortTimeString(),
                    Date = log.Created.ToShortDateString(),
                    Type = log.Type.ToString(),
                    Service = log.ServiceName,
                    Category = log.Category,
                    Message = log.Message
                });
            }
            
            return Collection(dtos);
        }

        [HttpPost("purge")]
        public Task Purge()
        {
            return _logsRepository.Purge();
        }

    }
}
