using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Sites
{
    [MicroService(ServiceNames.Sites)]
    public class SiteResourceCreated : IEvent
    {
    }
}
