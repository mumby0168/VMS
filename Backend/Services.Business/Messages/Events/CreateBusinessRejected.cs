using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Business.Messages.Events
{
    public class CreateBusinessRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public CreateBusinessRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
