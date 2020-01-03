using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Businesses
{
    [MicroService(Common.Names.Services.Businesses)]
    public class BusinessContactUpdated : IEvent
    {
    }
}
