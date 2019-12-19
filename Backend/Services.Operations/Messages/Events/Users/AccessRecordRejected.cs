using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Users
{
    [MicroService(ServiceNames.Users)]
    public class AccessRecordRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public AccessRecordRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
