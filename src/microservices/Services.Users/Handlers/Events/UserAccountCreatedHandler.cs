using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Users.Events;
using Services.Users.Factories;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Events
{
    public class UserAccountCreatedHandler : IEventHandler<UserAccountCreated>
    {
        private readonly IVmsLogger<UserAccountCreatedHandler> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IUsersFactory _factory;

        public UserAccountCreatedHandler(IVmsLogger<UserAccountCreatedHandler> logger, IAccountRepository accountRepository, IUsersFactory factory)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _factory = factory;
        }

        public async Task HandleAsync(UserAccountCreated message, IRequestInfo requestInfo)
        {
            await _accountRepository.AddAsync(_factory.CreateAccount(message.Id, message.Email));
            _logger.LogInformation($"Account added to users service with id: {message.Id} and email: {message.Email}");
        }
    }
}
