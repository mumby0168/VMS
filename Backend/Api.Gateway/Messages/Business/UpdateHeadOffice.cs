using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Business
{
    public class UpdateHeadOffice : ICommand
    {
        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string PostCode { get;  }

        [JsonConstructor]
        public UpdateHeadOffice(string addressLine1, string addressLine2, string postCode)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostCode = postCode;
        }
    }
}
