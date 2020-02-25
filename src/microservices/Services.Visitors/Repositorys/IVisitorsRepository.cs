using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Domain;

namespace Services.Visitors.Repositorys
{
    public interface IVisitorsRepository
    {
        Task AddAsync(IVisitor visitor);

        Task GetAsync(Guid visitorId);

        Task GetInForSiteAsync(Guid siteId);

        Task GetForSiteAsync(Guid siteId);
    }
}
