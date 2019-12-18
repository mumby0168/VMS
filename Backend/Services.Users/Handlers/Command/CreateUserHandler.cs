using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Users.Commands;
using Services.Users.Domain;
using Services.Users.Factories;
using Services.Users.Handlers.Events;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Command
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IVmsLogger<CreateUserHandler> _logger;
        private readonly IUsersFactory _factory;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IServiceBusMessagePublisher _publisher;

        public CreateUserHandler(IVmsLogger<CreateUserHandler> logger, IUsersFactory factory, IAccountRepository accountRepository, IUserRepository userRepository, IServiceBusMessagePublisher publisher)
        {
            _logger = logger;
            _factory = factory;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _publisher = publisher;
        }
        public async Task HandleAsync(CreateUser message, IRequestInfo requestInfo)
        {
            var account = await _accountRepository.GetAsync(message.AccountId);
            if(account is null)
            {
                _logger.LogWarning($"Account not found when completing user profile account id: {message.AccountId}.");
                _publisher.PublishEvent(new CreateUserRejected(Codes.InvalidId, $"The account with the id: {message.AccountId} cannot be found"), requestInfo);
            }

            //TODO: Call HTTP to check the site & the business ID. Need to use retry and integrate polly. IHttpClient from convey has built in retries.

            IUser user = null;
            try
            {
                user = _factory.CreateUser(message.FirstName, message.SecondName, account.Email, message.PhoneNumber,
                    message.BusinessPhoneNumber, message.BasedSiteId, message.BusinessId, message.AccountId);
            }
            catch (VmsException e)
            {
                _publisher.PublishEvent(new CreateUserRejected(e.Code, e.Message), requestInfo);
                return;
            }

            await _userRepository.AddAsync(user);
            _publisher.PublishEvent(new UserCreated(), requestInfo);
            _logger.LogInformation($"User created with id: {user.Id} and name: {user.FirstName + " " + user.SecondName}.");
        }
    }
}
