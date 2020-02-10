using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Businesses
{
    [MicroService(Common.Names.Services.Businesses)]
    public class UpdateBusinessRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public UpdateBusinessRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
