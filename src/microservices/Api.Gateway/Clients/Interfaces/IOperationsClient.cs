using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Dtos.Operations;

namespace Api.Gateway.Clients.Interfaces
{
    public interface IOperationsClient
    {
        Task<OperationDto> GetAsync(Guid id);
    }
}
