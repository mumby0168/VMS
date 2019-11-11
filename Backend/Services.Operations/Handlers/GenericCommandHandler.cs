using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Operations.Services;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Handlers
{
    public class GenericCommandHandler : IGenericCommandHandler
    {
        private readonly ILogger<GenericCommandHandler> _logger;
        private readonly IOperationsCache _cache;

        public GenericCommandHandler(ILogger<GenericCommandHandler> logger, IOperationsCache cache)
        {
            _logger = logger;
            _cache = cache;
        }
        public Task HandleAsync(object message, IRequestInfo requestInfo)
        {
            _logger.LogInformation($"Operation [{requestInfo.OperationId}]: PENDING");
            return _cache.SaveAsync(requestInfo.OperationId, requestInfo.State.ToString().ToLower());
        }
    }
}
