using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Users
{
    [MicroService(ServiceNames.Users)]
    public class CreateUserRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public CreateUserRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
