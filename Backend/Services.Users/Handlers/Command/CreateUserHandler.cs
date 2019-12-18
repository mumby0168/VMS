using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Users.Commands;
using Services.Users.Factories;

namespace Services.Users.Handlers.Command
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IVmsLogger<CreateUserHandler> _logger;
        private readonly IUsersFactory _factory;

        public CreateUserHandler(IVmsLogger<CreateUserHandler> logger, IUsersFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }
        public Task HandleAsync(CreateUser message, IRequestInfo requestInfo)
        {
            throw new NotImplementedException();
        }
    }
}
