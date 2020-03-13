using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Common.Queries;
using Services.Visitors.Dtos;
using Services.Visitors.Queries;

namespace Services.Visitors.Handlers.Queries
{
    public class VisitorRecordsForBusinessHandler : IQueryHandler<VisitorRecordsForBusiness, IEnumerable<VisitorRecordDto>>
    {
        public Task<IEnumerable<VisitorRecordDto>> HandleAsync(VisitorRecordsForBusiness query)
        {
            throw new System.NotImplementedException();
        }
    }
}