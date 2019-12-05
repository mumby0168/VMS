using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Logging;
using Services.Operations.Messages.Events.Push;
using Services.Operations.Services;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Services.Operations.Handlers
{
    public class GenericBusHandler : IGenericBusHandler
    {
        private readonly IVmsLogger<GenericBusHandler> _logger;
        private readonly IOperationsCache _operationsCache;
        private readonly IServiceBusMessagePublisher _publisher;

        public GenericBusHandler(IVmsLogger<GenericBusHandler> logger, IOperationsCache operationsCache, IServiceBusMessagePublisher publisher)
        {
            _logger = logger;
            _operationsCache = operationsCache;
            _publisher = publisher;
        }
        public async Task HandleAsync(object message, IRequestInfo requestInfo)
        {
            switch (message)
            {
                case IRejectedEvent rejected:
                    requestInfo.Fail();
                    _logger.LogInformation($"Operation [{requestInfo.OperationId}]: Rejected Event code: [{rejected.Code}] Reason: {rejected.Reason}");
                    await _operationsCache.SaveAsync(requestInfo.OperationId, requestInfo.State.ToString().ToLower(), rejected.Code, rejected.Reason);
                    _publisher.PublishEvent(new OperationFailed(rejected.Code, rejected.Reason), requestInfo);
                    break;
                case ICommand _:
                    _logger.LogInformation($"Operation [{requestInfo.OperationId}]: PENDING");
                    await _operationsCache.SaveAsync(requestInfo.OperationId, RequestState.Pending.ToString());
                    break;
                case IEvent _:
                    requestInfo.Complete();
                    _logger.LogInformation($"Operation: [{requestInfo.OperationId}] COMPLETE");
                    await _operationsCache.SaveAsync(requestInfo.OperationId, requestInfo.State.ToString().ToLower());
                    _publisher.PublishEvent(new OperationComplete(), requestInfo);
                    break;
                default:
                    break;
            }
        }

    }
}
