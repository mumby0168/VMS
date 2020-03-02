using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Visitors.Commands;

namespace Services.Visitors.Services
{
    public interface IVisitorFormValidatorService
    {
        Task Validate(Guid businessId, IEnumerable<VisitorDataEntry> data);
    }
}