using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Dtos.Visitors;

namespace Api.Gateway.Clients.Interfaces
{
    public interface IVisitorsClient
    {

        Task<IEnumerable<VisitorRecordDto>> GetVisitorsForBusinessAsync(Guid businessId);
        Task<IEnumerable<DataSpecificationDto>> GetDataSpecificationsForBusinessAsync(Guid businessId);
        Task<IEnumerable<string>> GetDataSpecificationValidatorsAsync();
        Task<IEnumerable<VisitorDto>> GetVisitorsForSiteAsync(Guid siteId);
        Task<IEnumerable<VisitorInformationDto>> GetDataForVisitorAsync(Guid visitorId);
        Task<IEnumerable<VisitorRecordDto>> GetVisitorsForUserAsync(Guid userId);
    }
}   
