using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    [Route("gateway/api/operations/")]
    public class OperationsController : GatewayControllerBase
    {
        private readonly IOperationsClient _operationsClient;

        public OperationsController(IDispatcher dispatcher, IOperationsClient operationsClient) : base(dispatcher)
        {
            _operationsClient = operationsClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id) => Single(await _operationsClient.GetAsync(id));
    }
}