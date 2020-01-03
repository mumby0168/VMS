using System;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Visitors
{
    [MicroService(Services.Common.Names.Services.Visitors)]
    public class CreateDataEntry : ICommand
    {
        public Guid BusinessId { get; }
        public string Label { get; }

        public string ValidationMessage { get; }

        public string ValidationCode { get; }

        [JsonConstructor]
        public CreateDataEntry(string label, string validationMessage, string validationCode, Guid businessId)
        {
            Label = label;
            ValidationMessage = validationMessage;
            ValidationCode = validationCode;
            BusinessId = businessId;
        }
    }
}
