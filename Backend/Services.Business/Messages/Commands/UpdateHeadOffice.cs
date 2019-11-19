using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Business.Messages.Commands
{
    public class UpdateHeadOffice : ICommand
    {
        public Guid BusinessId { get; }
        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string PostCode { get;  }

        [JsonConstructor]
        public UpdateHeadOffice(Guid businessId, string addressLine1, string addressLine2, string postCode)
        {
            BusinessId = businessId;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostCode = postCode;
        }
    }
}
