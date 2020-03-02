using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Events
{
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