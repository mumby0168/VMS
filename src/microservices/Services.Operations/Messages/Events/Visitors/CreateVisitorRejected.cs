using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Visitors
{
    [MicroService(Services.Common.Names.Services.Visitors)]
    public class CreateVisitorRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public CreateVisitorRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}