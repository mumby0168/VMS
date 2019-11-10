using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Extensions;
using Services.Common.Validation;

namespace Services.Businesses.Domain
{
    public class HeadOffice
    {
        public string PostCode { get; }

        public string AddressLine1 { get; }

        public string AddressLine2 { get; }

        public HeadOffice(string postCode, string addressLine1, string addressLine2)
        {
            if(!Validator.IsPostCodeValid(postCode)) throw new VmsException(Codes.InvalidPostCode, $"The postcode: {postCode} is not valid in the UK ");
            if(addressLine1.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The property address line 1 must be given a value.");

            PostCode = postCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
        }
    }
}
