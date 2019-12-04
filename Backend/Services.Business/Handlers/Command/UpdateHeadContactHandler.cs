using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Business.Messages.Commands;
using Services.Business.Messages.Events;
using Services.Business.Messages.Events.Rejected;
using Services.Business.Repositorys;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Services.Business.Handlers.Command
{
    public class UpdateHeadContactHandler : ICommandHandler<UpdateHeadContact>
    {
        private readonly IVmsLogger<UpdateHeadContactHandler> _logger;
        private readonly IBusinessRepository _repository;
        private readonly IServiceBusMessagePublisher _publisher;

        public UpdateHeadContactHandler(IVmsLogger<UpdateHeadContactHandler> logger, IBusinessRepository repository, IServiceBusMessagePublisher publisher)
        {
            _logger = logger;
            _repository = repository;
            _publisher = publisher;
        }
        public async Task HandleAsync(UpdateHeadContact message, IRequestInfo requestInfo)
        {
            var business = await _repository.GetBusinessAsync(message.BusinessId);
            if (business is null)
            {
                _publisher.PublishEvent(new UpdateBusinessRejected(Codes.InvalidId, "A business with the id: " + message.BusinessId + "could not be found"), requestInfo);
                return;
            }

            try
            {
                business.GetContact().Update(message.FirstName, message.SecondName, message.ContactNumber, message.Email);
            }
            catch (VmsException e)
            {
                _publisher.PublishEvent(new UpdateBusinessRejected(e.Code, e.Message), requestInfo);
                return;
            }

            await _repository.UpdateAsync(business);
            _publisher.PublishEvent(new BusinessContactUpdated(), requestInfo);
            _logger.LogInformation("The business contact was updated successfully.");
        }
    }
}
