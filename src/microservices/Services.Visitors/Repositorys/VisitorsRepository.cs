﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Domain.Domain.Visitor;

namespace Services.Visitors.Repositorys
{
    public class VisitorsRepository : IVisitorsRepository
    {
        private readonly IMongoRepository<VisitorDocument> _repository;

        public VisitorsRepository(IMongoRepository<VisitorDocument> repository)
        {
            _repository = repository;
        }
        
        public Task AddAsync(VisitorDocument visitorDocument)
        {
            return _repository.AddAsync(visitorDocument as VisitorDocument);
        }

        public Task<VisitorDocument> GetAsync(Guid visitorId)
        {
            return _repository.GetAsync(visitorId);
        }

        public Task GetInForSiteAsync(Guid siteId)
        {
            return _repository.FindAsync(v => v.VisitingSiteId == siteId && v.Status == VisitorStatus.In);
        }

        public Task<IEnumerable<VisitorDocument>> GetForSiteAsync(Guid siteId)
        {
            return _repository.FindAsync(v => v.VisitingSiteId == siteId && v.Status == VisitorStatus.In);
        }

        public Task UpdateAsync(VisitorDocument visitor)
        {
            return _repository.UpdateAsync(visitor, visitor.Id);
        }
    }
}
