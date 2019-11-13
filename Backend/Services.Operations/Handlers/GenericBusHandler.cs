using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Operations.Services;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Handlers
{
    public class GenericBusHandler : IGenericBusHandler
    {
        private readonly ILogger<GenericBusHandler> _logger;
        private readonly IOperationsCache _operationsCache;

        public GenericBusHandler(ILogger<GenericBusHandler> logger, IOperationsCache operationsCache)
        {
            _logger = logger;
            _operationsCache = operationsCache;
        }
        public async Task HandleAsync(object message, IRequestInfo requestInfo)
        {
            if (message is IRejectedEvent rejected)
            {
                requestInfo.Fail();
                _logger.LogInformation($"Operation [{requestInfo.OperationId}]: Rejected Event code: [{rejected.Code}] Reason: {rejected.Reason}");
                await _operationsCache.SaveAsync(requestInfo.OperationId, requestInfo.State.ToString().ToLower(), rejected.Code, rejected.Reason);
            }
            else if(message is ICommand)
            {
                _logger.LogInformation($"Operation [{requestInfo.OperationId}]: PENDING");
                await _operationsCache.SaveAsync(requestInfo.OperationId, requestInfo.State.ToString().ToLower());
            }
            else if(message is IEvent)
            {
                54.requestInfo.Complete();
                _logger.LogInformation($"Operation: [{requestInfo.OperationId}] COMPLETE");
                await _operationsCache.SaveAsync(requestInfo.OperationId, requestInfo.State.ToString().ToLower());
            }
        }

    }
}
