using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Messages.Events.Push
{
    public class OperationFailed : IEvent
    {
        public OperationFailed(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
        public string Code { get; }

        public string Reason { get; }
    }
}
