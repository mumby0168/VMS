using System;
using Services.Common.Queries;

namespace Services.Visitors.Queries
{
    public class VisitorRecordsForBusiness : IQuery
    {
        public Guid BusinessId { get; set; } 
    }
}