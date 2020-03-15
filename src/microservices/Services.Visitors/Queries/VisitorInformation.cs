using System;
using Services.Common.Queries;

namespace Services.Visitors.Queries
{
    public class VisitorInformation : IQuery
    {
        public Guid VisitorId { get; set; }
    }
}