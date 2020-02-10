using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Operations.Dtos;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Services.Operations.Services
{
    public interface IOperationsCache
    {
        Task SaveAsync(Guid id, string state, string code = null, string reason = null);

        Task<OperationDto> GetAsync(Guid id);
    }
}
    