using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Logging;
using Services.Push.Messages.Events;
using Services.Push.Services;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Push.Handlers
{
    public class PushServiceHandler : IGenericBusHandler
    {
        private readonly IVmsLogger<PushServiceHandler> _logger;
        private readonly IPushService _pushService;

        public PushServiceHandler(IVmsLogger<PushServiceHandler> logger, IPushService pushService)
        {
            _logger = logger;
            _pushService = pushService;
        }
        public async Task HandleAsync(object message, IRequestInfo requestInfo)
        {
            switch (message)
            {
                case OperationComplete complete:
                    _logger.LogInformation($"Operation [{requestInfo.OperationId}] completed and sent to user: {requestInfo.UserId}");
                    await _pushService.OperationComplete(requestInfo.OperationId, requestInfo.UserId);
                    break;
                case OperationFailed failed:
                    _logger.LogInformation($"Operation [{requestInfo.OperationId}] failed with code {failed.Code} and sent to user: {requestInfo.UserId}");
                    await _pushService.OperationFailed(requestInfo.OperationId, requestInfo.UserId, failed.Code, failed.Reason);
                    break;
                case OperationPending pending:
                    break;
                default:
                    break;
            }

        }
    }
}
