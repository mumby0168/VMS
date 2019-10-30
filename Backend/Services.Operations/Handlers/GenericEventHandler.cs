using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Handlers
{
    public class GenericEventHandler : IGenericEventHandler
    {
        private readonly ILogger<GenericEventHandler> _logger;

        public GenericEventHandler(ILogger<GenericEventHandler> logger)
        {
            _logger = logger;
        }
        public Task HandleAsync(object message, IRequestInfo requestInfo)
        {
            if (message is IRejectedEvent rejected)
            {
                _logger.LogInformation($"Rejected Event code: [{rejected.Code}] Reason: {rejected.Reason}");
            }
            else
            {
                _logger.LogInformation($"Operation: [{requestInfo.OperationId}] COMPLETE");
            }

            return Task.CompletedTask;
        }

    }
}
