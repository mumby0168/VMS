using System.Threading.Tasks;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Visitors.Commands;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Command
{
    public class CreateVisitorHandler : ICommandHandler<CreateVisitor>
    {
        private readonly IVmsLogger<CreateVisitorHandler> _logger;
        private readonly IVisitorsRepository _visitorsRepository;

        public CreateVisitorHandler(IVmsLogger<CreateVisitorHandler> logger, IVisitorsRepository visitorsRepository)
        {
            _logger = logger;
            _visitorsRepository = visitorsRepository;
        }
        public async Task HandleAsync(CreateVisitor message, IRequestInfo requestInfo)
        {
            
        }
    }
}