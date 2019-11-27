using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Sites.Rejected
{
    [MicroService(ServiceNames.Sites)]
    public class SiteResourceRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public SiteResourceRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
