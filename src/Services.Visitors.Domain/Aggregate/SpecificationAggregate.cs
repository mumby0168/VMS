using System;
using System.Linq;
using Services.Common.Exceptions;
using Services.Common.Extensions;

using Services.Visitors.Domain.Domain.Specification;

namespace Services.Visitors.Domain.Aggregate
{
    public class SpecificationAggregate : ISpecificationAggregate
    {
        public SpecificationDocument Create(string label, int order, string validationMessage, string validationCode, Guid businessId, bool isMandatory = false)
        {
            if (label.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The label cannot be empty."); 

            if (!Validation.Options.Contains(validationCode)) throw new VmsException(Codes.InvalidValidationCode, $"The validation code {validationCode} is not valid.");

            if(validationMessage.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The validation message cannot be empty.");

            return new SpecificationDocument()
            {
                Id = Guid.NewGuid(),
                Label = label,
                Order = order,
                ValidationMessage = validationMessage,
                ValidationCode = validationCode,
                BusinessId = businessId,
                IsMandatory = isMandatory,
                IsLive = true
            };
        }

        public void Deprecate(SpecificationDocument specificationDocument)
        {
            specificationDocument.IsLive = false;
        }

        public void UpdateOrder(SpecificationDocument specificationDocument, int order)
        {
            specificationDocument.Order = order;
        }
    }
}