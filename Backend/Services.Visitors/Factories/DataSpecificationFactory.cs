using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Domain;

namespace Services.Visitors.Factories
{
    public class DataSpecificationFactory : IDataSpecificationFactory
    {
        public IDataSpecification Create(string label, int order, string validationMessage, string validationCode, Guid businessId)
        {
            return new DataSpecification().Setup(label, order, validationMessage, validationCode, businessId);
        }
    }
}
