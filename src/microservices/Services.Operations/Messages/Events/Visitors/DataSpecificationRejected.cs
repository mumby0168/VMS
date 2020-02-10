using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Visitors
{
    [MicroService(Common.Names.Services.Visitors)]
    public class DataSpecificationRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public DataSpecificationRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
