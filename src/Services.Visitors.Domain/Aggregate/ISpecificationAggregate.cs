using System;
using Services.Visitors.Domain.Domain.Specification;

namespace Services.Visitors.Domain.Aggregate
{
    public interface ISpecificationAggregate
    {
        SpecificationDocument Create(string label, int order, string validationMessage, string validationCode,
            Guid businessId, bool isMandatory = false);

        void Deprecate(SpecificationDocument specificationDocument);

        void UpdateOrder(SpecificationDocument specificationDocument, int order);
    }
}