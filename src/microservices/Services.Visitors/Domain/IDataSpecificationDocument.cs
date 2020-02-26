using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Visitors.Domain
{
    public interface IDataSpecificationDocument : IDomain
    {
        IDataSpecificationDocument Setup(string label,int order, string validationMessage, string validationCode, Guid businessId);

        void UpdateOrder(int newOrder);
        string Label { get; }

        int Order { get; }

        string ValidationMessage { get; }

        string ValidationCode { get; }

        bool IsLive { get; }

        Guid BusinessId { get; }

        void Deprecate();
    }
}
