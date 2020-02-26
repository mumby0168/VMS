using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Domain;

namespace Services.Visitors.Factories
{
    public interface IDataSpecificationFactory
    {
        IDataSpecificationDocument Create(string label, int order, string validationMessage, string validationCode, Guid businessId);
    }
}
