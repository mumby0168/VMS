using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Dtos.Operations
{
    public class OperationDto
    {
        public Guid Id { get; set; }

        public string State { get; set; }

        public string Code { get; set; }

        public string Reason { get; set; }
    }
}
