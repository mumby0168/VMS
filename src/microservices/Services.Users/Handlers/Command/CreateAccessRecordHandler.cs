using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Services.Common.Logging;
using Services.Common.Mongo;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Users.Commands;
using Services.Users.Domain;
using Services.Users.Events;
using Services.Users.Factories;
using Services.Users.Repositories;
using Services.Users.Services;

namespace Services.Users.Handlers.Command
{
    public class CreateAccessRecordHandler : ICommandHandler<CreateAccessRecord>
    {
        private readonly IVmsLogger<CreateAccessRecordHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAccessRecordRepository _accessRecordRepository;
        private readonly IAccessRecordFactory _factory;
        private readonly IServiceBusMessagePublisher _publisher;
        private readonly IServicesRepository _servicesRepository;        
        private readonly IUserStatusService _userStatusService;

        public CreateAccessRecordHandler(IVmsLogger<CreateAccessRecordHandler> logger, IUserRepository userRepository,
            IAccessRecordRepository accessRecordRepository, IAccessRecordFactory factory, IServiceBusMessagePublisher publisher, IServicesRepository servicesRepository
            ,IUserStatusService userStatusService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _accessRecordRepository = accessRecordRepository;
            _factory = factory;
            _publisher = publisher;
            _servicesRepository = servicesRepository;            
            _userStatusService = userStatusService;
        }

        public async Task HandleAsync(CreateAccessRecord message, IRequestInfo requestInfo)
        {
            IUserDocument userDocument = await _userRepository.GetByCodeAsync(message.Code);
            if(userDocument is null)
            {
                _publisher.PublishEvent(new AccessRecordRejected(Codes.InvalidId, "The code used is invalid."), requestInfo);
                _logger.LogError($"User with code: {message.Code} could not be found.");
                return;  
            }

            
            if(!await _servicesRepository.IsSiteIdValid(message.SiteId))
            {
                _publisher.PublishEvent(new AccessRecordRejected(Codes.InvalidId, "The site could not be found."), requestInfo);
                _logger.LogError($"Site with id: {message.SiteId} could not be found.");
                return;
            }

            await _userStatusService.Update(userDocument.Id, message.Action, message.SiteId);

            var record = _factory.Create(userDocument.Id, message.SiteId, message.Action, userDocument.BusinessId);
            await _accessRecordRepository.AddAsync(record);
            _publisher.PublishEvent(new AccessRecordCreated(), requestInfo);

            string action = message.Action == AccessAction.In ? "in" : "out";

            _logger.LogInformation($"{userDocument.FirstName + " " + userDocument.SecondName} signed {action} on : {record.TimeStamp}.", LoggingCategories.Access);
        }
    }
}
