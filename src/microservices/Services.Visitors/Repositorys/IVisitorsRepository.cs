using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Domain;
using Services.Visitors.Domain.Domain.Visitor;

namespace Services.Visitors.Repositorys
{
    public interface IVisitorsRepository
    {
        Task AddAsync(VisitorDocument visitorDocument);

        Task<VisitorDocument> GetAsync(Guid visitorId);

        Task GetInForSiteAsync(Guid siteId);

        Task<IEnumerable<VisitorDocument>> GetForSiteAsync(Guid siteId);
        Task UpdateAsync(VisitorDocument visitor);
        Task<IEnumerable<VisitorDocument>> GetVistorsForBusinessAsync(Guid businessId);
    }
}
    