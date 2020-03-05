using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Events
{
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