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
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Services.Business.Handlers.Command
{
    public class UpdateHeadOfficeHandler : ICommandHandler<UpdateHeadOffice>
    {
        private readonly ILogger<UpdateHeadOfficeHandler> _logger;
        private readonly IBusinessRepository _repository;
        private readonly IServiceBusMessagePublisher _publisher;

        public UpdateHeadOfficeHandler(ILogger<UpdateHeadOfficeHandler> logger, IBusinessRepository repository, IServiceBusMessagePublisher publisher)
        {
            _logger = logger;
            _repository = repository;
            _publisher = publisher;
        }
        public async Task HandleAsync(UpdateHeadOffice message, IRequestInfo requestInfo)
        {
            var business = await _repository.GetBusinessAsync(message.BusinessId);
            if (business is null)
            {
                _publisher.PublishEvent(new UpdateBusinessRejected(Codes.InvalidId, "A business with the id: " + message.BusinessId + "could not be found"), requestInfo);
                return;
            }

            try
            {
                business.GetOffice().Update(message.PostCode, message.AddressLine1, message.AddressLine2);
            }
            catch (VmsException e)
            {
                _publisher.PublishEvent(new UpdateBusinessRejected(e.Code, e.Message), requestInfo);
                return;
            }

            await _repository.UpdateAsync(business);
            _publisher.PublishEvent(new BusinessOfficeUpdated(), requestInfo);
            _logger.LogInformation("The business head office was updated successfully.");
        }
    }
}
