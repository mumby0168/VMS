using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Extensions;
using Services.Visitors.Names;

namespace Services.Visitors.Domain
{
    public class DataSpecification : IDataSpecification
    {
        public Guid Id { get; private set; }
        public IDataSpecification Setup(string label,int order, string validationMessage, string validationCode, Guid businessId)
        {
            return new DataSpecification(label, order, validationMessage, validationCode, businessId);
        }

        public void UpdateOrder(int newOrder)
        {
            Order = newOrder;
        }

        public string Label { get; private set; }
        public int Order { get; private set; }
        public string ValidationMessage { get; private set; }
        public string ValidationCode { get; private set; }
        public bool IsLive { get; private set; }
        public Guid BusinessId { get; private set; }

        public void Deprecate()
        {
            IsLive = false;
        }

        public DataSpecification()
        {
            
        }

        private DataSpecification(string label, int order, string validationMessage, string validationCode,
            Guid businessId)
        {

            if (label.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The label cannot be empty."); 

            if (!Validation.Options.Contains(validationCode)) throw new VmsException(Codes.InvalidValidationCode, $"The validation code {validationCode} is not valid.");

            if(validationMessage.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The validation message cannot be empty.");


            Id = Guid.NewGuid();
            BusinessId = businessId;
            Label = label;
            Order = order;
            ValidationMessage = validationMessage;
            ValidationCode = validationCode;
            IsLive = true;
        }
    }
}
