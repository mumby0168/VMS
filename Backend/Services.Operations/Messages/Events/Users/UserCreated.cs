using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Users
{
    [MicroService(ServiceNames.Users)]
    public class UserCreated : IEvent
    {
    }
}
