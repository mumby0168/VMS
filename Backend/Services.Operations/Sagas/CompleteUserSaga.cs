using System;
using System.Linq;
using System.Threading.Tasks;
using Chronicle;
using Services.Common.Logging;
using Services.Operations.Handlers;
using Services.Operations.Messages.Events.Identity;
using Services.Operations.Sagas.Commands;
using Services.RabbitMq.Messages;

namespace Services.Operations.Sagas
{
    public class CompleteUserSaga : Saga, ISagaStartAction<UserAccountCompleted>
    {
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly IVmsLogger<CompleteUserSaga> _logger;

        public CompleteUserSaga(IServiceBusMessagePublisher publisher, IVmsLogger<CompleteUserSaga> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }


        public Task HandleAsync(UserAccountCompleted message, ISagaContext context)
        {
            var operationId = Guid.Parse(context.Metadata.FirstOrDefault(m => m.Key == SagaData.OperationIdKey).Key);
            var userId = Guid.Parse(context.Metadata.FirstOrDefault(m => m.Key == SagaData.UserIdKey).Key);

            _logger.LogInformation("Got user completed in saga operation id: " + operationId);

            _publisher.PublishCommand(new CreateUser(message.FirstName, message.SecondName, message.PhoneNumber, message.BusinessPhoneNumber, message.BasedSiteId, message.BusinessId), new RequestInfo(operationId, userId));

            return Task.CompletedTask;
        }

        public Task CompensateAsync(UserAccountCompleted message, ISagaContext context)
        {
            return  Task.CompletedTask;
        }
    }
}
