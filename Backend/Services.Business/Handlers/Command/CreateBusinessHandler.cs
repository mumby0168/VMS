using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Businesses.Domain;
using Services.Businesses.Factories;
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
        private readonly IBusinessesFactory _businessesFactory;

        public CreateBusinessHandler(IBusinessRepository repository, ILogger<CreateBusinessHandler> logger, IServiceBusMessagePublisher publisher, IBusinessesFactory businessesFactory)
        {
            _repository = repository;
            _logger = logger;
            _publisher = publisher;
            _businessesFactory = businessesFactory;
        }
        public async Task HandleAsync(CreateBusiness message, IRequestInfo requestInfo)
        {
            if (message == null)
            {
                _logger.LogError("The message read was null request: " + requestInfo.OperationId);
                return;
            }
            Business business;
            try
            {
                business = _businessesFactory.CreateBusiness(message.Name, message.TradingName, message.WebAddress,
                    message.HeadContactFirstName, message.HeadContactSecondName,
                    message.HeadContactContactNumber, message.HeadContactEmail, message.HeadOfficePostCode, message.HeadOfficeAddressLine1, message.HeadOfficeAddressLine2);

                await _repository.Add(business);
            }
            catch (VmsException e)
            {
                _logger.LogInformation("Create business rejected: " + e.Code);
                _publisher.PublishEvent(new CreateBusinessRejected(e.Code, e.Message), requestInfo);
                return;
            }

            _logger.LogInformation("Create business succeeded.");
            _publisher.PublishEvent(new BusinessCreated(business.Id), requestInfo);
        }
    }
}
