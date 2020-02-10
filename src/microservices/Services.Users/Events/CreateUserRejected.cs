using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Users.Events
{
    public class CreateUserRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }


        public CreateUserRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
