using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Identity
{
    public class UserAccountRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public UserAccountRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
