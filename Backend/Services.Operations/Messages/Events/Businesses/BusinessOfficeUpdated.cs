using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Businesses
{
    [MicroService(ServiceNames.Businesses)]
    public class BusinessOfficeUpdated : IEvent
    {
    }
}
