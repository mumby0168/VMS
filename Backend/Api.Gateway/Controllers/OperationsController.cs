using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Controllers
{
    public class OperationsController : GatewayControllerBase
    {
        public OperationsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }
    }
}