using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Users.Events;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Events
{
    public class AccountRemovedHandler : IEventHandler<AccountRemoved>
    {
        private readonly IVmsLogger<AccountRemovedHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAccessRecordRepository _accessRecordRepository;

        public AccountRemovedHandler(IVmsLogger<AccountRemovedHandler> logger, IUserRepository userRepository, IAccessRecordRepository accessRecordRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _accessRecordRepository = accessRecordRepository;
        }
        public async Task HandleAsync(AccountRemoved message, IRequestInfo requestInfo)
        {
            var user = await _userRepository.GetFromAccountId(message.AccountId);

            if (user is null)
            {
                //TODO: What to do here how to notify user? Possibly do nothing. Critical log should be raised.
                return;
            }

            user.SuspendAccount();
            await _userRepository.UpdateAsync(user);
            await _accessRecordRepository.RemoveRangeByUserId(user.Id);
        }
    }
}
