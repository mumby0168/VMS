using System.Threading.Tasks;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Domain.Aggregate;
using Services.Visitors.Events;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Handlers.Command
{
    public class VisitorSignOutHandler : ICommandHandler<VisitorSignOut>
    {
        private readonly IVmsLogger<VisitorSignOutHandler> _logger;
        private readonly IVisitorsRepository _visitorsRepository;
        private readonly IVisitorAggregate _visitorAggregate;
        private readonly IServiceBusMessagePublisher _publisher;

        public VisitorSignOutHandler(IVmsLogger<VisitorSignOutHandler> logger, IVisitorsRepository visitorsRepository, IVisitorAggregate visitorAggregate, IServiceBusMessagePublisher publisher)
        {
            _logger = logger;
            _visitorsRepository = visitorsRepository;
            _visitorAggregate = visitorAggregate;
            _publisher = publisher;
        }

        public async Task HandleAsync(VisitorSignOut message, IRequestInfo requestInfo)
        {
            var visitor = await _visitorsRepository.GetAsync(message.VisitorId);

            if (visitor is null)
            {
                _logger.LogWarning($"No visitor found with ID : ${message.VisitorId}");
                _publisher.PublishEvent(new VisitorSignOutRejected(Codes.InvalidId, "You could not be signed out."), requestInfo);
            }

            _visitorAggregate.SignOut(visitor);
            await _visitorsRepository.UpdateAsync(visitor);
            
            _logger.LogInformation($"Visitor with id: {message.VisitorId} signed out successfully");
            _publisher.PublishEvent(new VisitorSignedOut(), requestInfo);
        }
    }
}