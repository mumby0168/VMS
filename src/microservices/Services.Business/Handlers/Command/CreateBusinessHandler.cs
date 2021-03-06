﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Business.Domain;
using Services.Business.Factories;
using Services.Business.Messages.Commands;
using Services.Business.Messages.Events;
using Services.Business.Messages.Events.Rejected;
using Services.Business.Repositorys;
using Services.Common.Exceptions;
using Services.Common.Generation;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Services.Business.Handlers.Command
{
    public class CreateBusinessHandler : ICommandHandler<CreateBusiness>
    {
        private readonly IBusinessRepository _repository;
        private readonly IVmsLogger<CreateBusinessHandler> _logger;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly IBusinessesFactory _businessesFactory;
        private readonly INumberGenerator _numberGenerator;
        private int _checks = 0;

        public CreateBusinessHandler(IBusinessRepository repository, IVmsLogger<CreateBusinessHandler> logger, IServiceBusMessagePublisher publisher, IBusinessesFactory businessesFactory, INumberGenerator numberGenerator)
        {
            _repository = repository;
            _logger = logger;
            _publisher = publisher;
            _businessesFactory = businessesFactory;
            _numberGenerator = numberGenerator;
        }
        public async Task HandleAsync(CreateBusiness message, IRequestInfo requestInfo)
        {
            IBusinessDocument businessDocument;

            try
            {
                var code = await CheckCode(_numberGenerator.GenerateNumber(6));
                businessDocument = _businessesFactory.CreateBusiness(message.Name, message.TradingName, message.WebAddress,
                    message.HeadContactFirstName, message.HeadContactSecondName,
                    message.HeadContactContactNumber, message.HeadContactEmail, message.HeadOfficePostCode, message.HeadOfficeAddressLine1, message.HeadOfficeAddressLine2, code);


                await _repository.Add(businessDocument);
            }
            catch (VmsException e)
            {
                _logger.LogInformation("Create business rejected: " + e.Code);
                _publisher.PublishEvent(new CreateBusinessRejected(e.Code, e.Message), requestInfo);
                return;
            }

            _logger.LogInformation("Create business succeeded.");
            _publisher.PublishEvent(new BusinessCreated(businessDocument.Id, businessDocument.Code), requestInfo);
        }

        private async Task<int> CheckCode(int code)
        {
            if(_checks == 6) throw new VmsException(Codes.InvalidId, "A unique code for this business could not be created.");
            var number = _numberGenerator.GenerateNumber(6);
            if (await _repository.IsCodeInUseAsync(number))
            {
                _checks++;
                number = _numberGenerator.GenerateNumber(6);
                await CheckCode(number);
            }

            return number;
        }
    }
}
