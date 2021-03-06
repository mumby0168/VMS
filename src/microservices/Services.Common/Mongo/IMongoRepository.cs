﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Common.Mongo
{ 
    public interface IMongoRepository<T> where T : IDomain
    {
        Task<T> GetAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task UpdateAsync(T newEntity, Guid id);

        Task RemoveAsync(Guid id);

        Task RemoveRangeAsync(Expression<Func<T, bool>> predicate);
    }
}
