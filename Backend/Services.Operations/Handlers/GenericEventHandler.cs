using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Operations.Services;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Handlers
{
    public class GenericEventHandler : IGenericEventHandler
    {
        private readonly ILogger<GenericEventHandler> _logger;
        private readonly IOperationsCache _operationsCache;

        public GenericEventHandler(ILogger<GenericEventHandler> logger, IOperationsCache operationsCache)
        {
            _logger = logger;
            _operationsCache = operationsCache;
        }
        public Task HandleAsync(object message, IRequestInfo requestInfo)
        {
            if (message is IRejectedEvent rejected)
            {
                requestInfo.Fail();
                _logger.LogInformation($"Operation [{requestInfo.OperationId}]: Rejected Event code: [{rejected.Code}] Reason: {rejected.Reason}");
                _operationsCache.SaveAsync(requestInfo.OperationId, requestInfo.State.ToString().ToLower(), rejected.Code, rejected.Reason);
            }
            else
            {
                requestInfo.Complete();
                _logger.LogInformation($"Operation: [{requestInfo.OperationId}] COMPLETE");
                _operationsCache.SaveAsync(requestInfo.OperationId, requestInfo.State.ToString().ToLower());
            }

            return Task.CompletedTask;
        }

    }
}
