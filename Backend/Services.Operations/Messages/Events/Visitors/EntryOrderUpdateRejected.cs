using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Visitors
{
    public class EntryOrderUpdateRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public EntryOrderUpdateRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
