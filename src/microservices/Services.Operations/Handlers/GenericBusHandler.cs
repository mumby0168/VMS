using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronicle;
using Microsoft.Extensions.Logging;
using Services.Common.Logging;
using Services.Operations.Messages.Events.Push;
using Services.Operations.Sagas;
using Services.Operations.Services;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Services.Operations.Handlers
{
    public class SagaData : ISagaContextMetadata
    {
        public static string OperationIdKey => "OperationId";
        public static string UserIdKey => "UserId";
        public string Key { get; }
        public object Value { get; }

        public SagaData(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }

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
            //var serviceBusMessage = message as IServiceBusMessage;
            //if (serviceBusMessage.BelongsToSaga())
            //{
            //    var context = SagaContext.Create(new SagaId(), "", new List<ISagaContextMetadata>
            //    {
            //        new SagaData(SagaData.OperationIdKey, requestInfo.OperationId.ToString()),
            //        new SagaData(SagaData.UserIdKey, requestInfo.UserId)
            //    });
            //    await _sagaCoordinator.ProcessAsync(serviceBusMessage, context);
            //    return;
            //}


            switch (message)
            {
                case ICommand _:
                    _logger.LogInformation($"Operation [{requestInfo.OperationId}]: PENDING");
                    await _operationsCache.SaveAsync(requestInfo.OperationId, RequestState.Pending.ToString());
                    break;
                case IRejectedEvent rejected:
                    requestInfo.Fail();
                    _logger.LogInformation($"Operation [{requestInfo.OperationId}]: Rejected Event code: [{rejected.Code}] Reason: {rejected.Reason}");
                    await _operationsCache.SaveAsync(requestInfo.OperationId, requestInfo.State.ToString().ToLower(), rejected.Code, rejected.Reason);
                    _publisher.PublishEvent(new OperationFailed(rejected.Code, rejected.Reason), requestInfo);
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
