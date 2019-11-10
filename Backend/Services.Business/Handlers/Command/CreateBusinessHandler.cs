using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Businesses.Domain;
using Services.Businesses.Handlers.Events;
using Services.Businesses.Messages.Commands;
using Services.Businesses.Repositorys;
using Services.Common.Exceptions;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Services.Businesses.Handlers.Command
{
    public class CreateBusinessHandler : ICommandHandler<CreateBusiness>
    {
        private readonly IBusinessRepository _repository;
        private readonly ILogger<CreateBusinessHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;

        public CreateBusinessHandler(IBusinessRepository repository, ILogger<CreateBusinessHandler> logger, IServiceBusMessagePublisher publisher)
        {
            _repository = repository;
            _logger = logger;
            _publisher = publisher;
        }
        public async Task HandleAsync(CreateBusiness message, IRequestInfo requestInfo)
        {
            try
            {
                var business = new Business(message.Name, message.TradingName, message.WebAddress,
                    new HeadOffice(message.HeadOfficePostCode, message.HeadOfficeAddressLine1,
                        message.HeadOfficeAddressLine2),
                    new HeadContact(message.HeadContactFirstName, message.HeadContactSecondName,
                        message.HeadContactContactNumber, message.HeadContactEmail));
                await _repository.Add(business);
            }
            catch (VmsException e)
            {
                _publisher.PublishEvent(new CreateBusinessRejected(e.Code, e.Message), requestInfo);
                return;
            }

            _publisher.PublishEvent(new BusinessCreated(), requestInfo);
        }
    }
}
