using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Businesses.Messages.Commands;
using Services.Businesses.Repositorys;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Businesses.Handlers.Command
{
    public class CreateBusinessHandler : ICommandHandler<CreateBusiness>
    {
        private readonly IBusinessRepository _repository;
        private readonly ILogger<CreateBusinessHandler> _logger;

        public CreateBusinessHandler(IBusinessRepository repository, ILogger<CreateBusinessHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public Task HandleAsync(CreateBusiness message, IRequestInfo requestInfo)
        {
            _logger.LogInformation("Created business with name: " + message.Name);
            return Task.CompletedTask;
        }
    }
}
