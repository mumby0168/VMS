using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Visitors.Domain;

namespace Services.Visitors.Repositorys
{
    public class VisitorsRepository : IVisitorsRepository
    {
        private readonly IMongoRepository<VisitorDocument> _repository;

        public VisitorsRepository(IMongoRepository<VisitorDocument> repository)
        {
            _repository = repository;
        }
        
        public Task AddAsync(IVisitorDocument visitorDocument)
        {
            return _repository.AddAsync(visitorDocument as VisitorDocument);
        }

        public Task GetAsync(Guid visitorId)
        {
            return _repository.GetAsync(visitorId);
        }

        public Task GetInForSiteAsync(Guid siteId)
        {
            return _repository.FindAsync(v => v.VisitingSiteId == siteId && v.Status == VisitorStatus.In);
        }

        public Task GetForSiteAsync(Guid siteId)
        {
            return _repository.FindAsync(v => v.VisitingUserId == siteId);
        }
    }
}
