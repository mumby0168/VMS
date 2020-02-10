using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Commands
{
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
