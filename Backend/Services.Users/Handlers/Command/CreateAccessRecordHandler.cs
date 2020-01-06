using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Users.Commands;
using Services.Users.Domain;
using Services.Users.Events;
using Services.Users.Factories;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Command
{
    public class CreateAccessRecordHandler : ICommandHandler<CreateAccessRecord>
    {
        private readonly IVmsLogger<CreateAccessRecordHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAccessRecordRepository _accessRecordRepository;
        private readonly IAccessRecordFactory _factory;
        private readonly IServiceBusMessagePublisher _publisher;

        public CreateAccessRecordHandler(IVmsLogger<CreateAccessRecordHandler> logger, IUserRepository userRepository, IAccessRecordRepository accessRecordRepository, IAccessRecordFactory factory, IServiceBusMessagePublisher publisher)
        {
            _logger = logger;
            _userRepository = userRepository;
            _accessRecordRepository = accessRecordRepository;
            _factory = factory;
            _publisher = publisher;

        }

        public async Task HandleAsync(CreateAccessRecord message, IRequestInfo requestInfo)
        {
            var user = await _userRepository.GetAsync(message.UserId);
            if(user is null)
            {
                _publisher.PublishEvent(new AccessRecordRejected(Codes.InvalidId, "No user could be found to insert access record."), requestInfo);
                _logger.LogError($"User with id: {message.UserId} could not be found.");
                return;
            }

            var record = _factory.Create(message.UserId, user.BasedSiteId, message.Action, user.BusinessId);
            await _accessRecordRepository.AddAsync(record);
            _publisher.PublishEvent(new AccessRecordCreated(), requestInfo);

            string action = message.Action == AccessAction.In ? "in" : "out";

            _logger.LogInformation($"{user.FirstName + " " + user.SecondName} signed {action} on : {record.TimeStamp}.", LoggingCategories.Access);
        }
    }
}
