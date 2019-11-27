using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Extensions;
using Services.Common.Validation;

namespace Services.Sites.Domain
{
    public class Site : ISite
    {
        public Guid Id { get; private set; }

       
        public string Name { get; set; }
        public Contact Contact { get; private set; }
        public string PostCode { get; private set; }

        public string AddressLine1 { get; private set; }

        public string AddressLine2 { get; private set; }
        public IContact GetContact() => Contact;

        public ISite Setup(Guid businessId, string name, string postCode, string addressLine1, string addressLine2, IContact contact)
        {
            if(string.IsNullOrWhiteSpace(name)) throw new VmsException("empty_property", "The field name must contain a value.");
            if (!Validator.IsPostCodeValid(postCode)) throw new VmsException(Codes.InvalidPostCode, $"The postcode: {postCode} is not valid in the UK ");
            if (addressLine1.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The property address line 1 must be given a value.");

            Id = Guid.NewGuid();
            BusinessId = businessId;
            Name = name;
            PostCode = postCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Contact = contact as Contact;
            return this;
        }

        public void Update(string name, string postCode, string addressLine1, string addressLine2)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new VmsException("empty_property", "The field name must contain a value.");
            if (!Validator.IsPostCodeValid(postCode)) throw new VmsException(Codes.InvalidPostCode, $"The postcode: {postCode} is not valid in the UK ");
            if (addressLine1.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The property address line 1 must be given a value.");

            Name = name;
            PostCode = postCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
        }

        public Guid BusinessId { get; private set; }
    }
}
