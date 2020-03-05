using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Visitors
{
    [MicroService(Common.Names.Services.Visitors)]
    public class VisitorSignOutRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public VisitorSignOutRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}