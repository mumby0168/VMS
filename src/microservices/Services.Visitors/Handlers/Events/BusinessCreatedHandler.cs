using System.Threading.Tasks;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Visitors.Domain.Aggregate;
using Services.Visitors.Events.Subscribed;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Events
{
    public class BusinessCreatedHandler : IEventHandler<BusinessCreated>
    {
        private readonly IVmsLogger<BusinessCreatedHandler> _logger;
        private readonly ISpecificationRepository _repository;
        private readonly ISpecificationAggregate _specificationAggregate;

        public BusinessCreatedHandler(IVmsLogger<BusinessCreatedHandler> logger,ISpecificationRepository repository, ISpecificationAggregate specificationAggregate)
        {
            _logger = logger;
            _repository = repository;
            _specificationAggregate = specificationAggregate;
        }
        
        public async Task HandleAsync(BusinessCreated message, IRequestInfo requestInfo)
        {
            var spec = _specificationAggregate.Create("Full Name", 1, "Please enter your name.", "Required", message.Id,
                true);
            await _repository.AddAsync(spec);
            _logger.LogInformation($"Mandatory name field added for business with id: {message.Id}");
        }
    }
}