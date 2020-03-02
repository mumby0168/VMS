using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Domain.Aggregate;
using Services.Visitors.Domain.Domain.Visitor;
using Services.Visitors.Events;
using Services.Visitors.Factories;
using Services.Visitors.Repositorys;
using Services.Visitors.Services;

namespace Services.Visitors.Handlers.Command
{
    public class CreateVisitorHandler : ICommandHandler<CreateVisitor>
    {
        private readonly IVmsLogger<CreateVisitorHandler> _logger;
        private readonly IVisitorsRepository _visitorsRepository;
        private readonly IUserServiceClient _userServiceClient;
        private readonly ISiteServiceClient _siteServiceClient;
        private readonly IVisitorAggregate _visitorAggregate;
        private readonly IServiceBusMessagePublisher _messagePublisher;
        private readonly IVisitorFormValidatorService _validatorService;

        public CreateVisitorHandler(IVmsLogger<CreateVisitorHandler> logger, IVisitorsRepository visitorsRepository, IUserServiceClient userServiceClient, ISiteServiceClient siteServiceClient, IVisitorAggregate visitorAggregate, IServiceBusMessagePublisher messagePublisher, IVisitorFormValidatorService validatorService)
        {
            _logger = logger;
            _visitorsRepository = visitorsRepository;
            _userServiceClient = userServiceClient;
            _siteServiceClient = siteServiceClient;
            _visitorAggregate = visitorAggregate;
            _messagePublisher = messagePublisher;
            _validatorService = validatorService;
        }
        
        public async Task HandleAsync(CreateVisitor message, IRequestInfo requestInfo)
        {
            if (!await _userServiceClient.ContainsUserAsync(message.VisitingId))
            {
                _logger.LogWarning($"No user with id: {message.VisitingId} could be found");
                PublishFailure(requestInfo);
                return;
            }

            var site = await _siteServiceClient.GetSiteAsync(message.SiteId);
            if (site is null)
            {
                _logger.LogInformation($"No site found with id: {message.SiteId}");
                PublishFailure(requestInfo);
                return;
            }

            try
            {
                await _validatorService.Validate(site.BusinessId, message.Data);
            }
            catch (VmsException e)
            {
                _messagePublisher.PublishEvent(new CreateVisitorRejected(e.Code, e.Message), requestInfo);
                return;
            }
            
            //TODO: create service to validate data entrys

            var visitorData = new List<VisitorData>();
            
            foreach (var visitorDataEntry in message.Data) visitorData.Add(_visitorAggregate.CreateData(visitorDataEntry.FieldId, visitorDataEntry.Value));

            var visitor = _visitorAggregate.Create(message.VisitingId, site.BusinessId, message.SiteId, visitorData);
            
            await _visitorsRepository.AddAsync(visitor);
            
            _messagePublisher.PublishEvent(new VisitorCreated(), requestInfo);
        }

        private void PublishFailure(IRequestInfo requestInfo)
        {
            _messagePublisher.PublishEvent(new CreateVisitorRejected(Codes.InvalidId, "Something went wrong sorry. Please speak to a staff member"), requestInfo);
        }
    }
}