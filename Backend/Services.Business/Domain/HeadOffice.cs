using Services.Common.Exceptions;
using Services.Common.Extensions;
using Services.Common.Validation;

namespace Services.Business.Domain
{
    public class HeadOffice
    {
        public string PostCode { get; private set; }

        public string AddressLine1 { get; private set; }

        public string AddressLine2 { get; private set; }

        public HeadOffice(string postCode, string addressLine1, string addressLine2)
        {
            ValidateAndUpdate(postCode, addressLine1, addressLine2);
        }

        public void Update(string postCode, string addressLine1, string addressLine2)
        {
            ValidateAndUpdate(postCode, addressLine1, addressLine2);
        }

        private void ValidateAndUpdate(string postCode, string addressLine1, string addressLine2)
        {
            if (!Validator.IsPostCodeValid(postCode)) throw new VmsException(Codes.InvalidPostCode, $"The postcode: {postCode} is not valid in the UK ");
            if (addressLine1.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The property address line 1 must be given a value.");

            PostCode = postCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
        }
    }
}
