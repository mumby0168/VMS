using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Common.Domain;

namespace Services.Common.Mongo
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IDomain
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoManager mongoManager)
        {
            _collection = mongoManager.GetCollection<T>();
        }


        public Task<T> GetAsync(Guid id) => _collection.AsQueryable().FirstOrDefaultAsync(e => e.Id == id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _collection.AsQueryable().ToListAsync();

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate) =>
            _collection.AsQueryable().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _collection.Find(predicate).ToListAsync();

        public Task AddAsync(T entity) => _collection.InsertOneAsync(entity);

        public Task UpdateAsync(T newEntity, Guid id) => _collection.ReplaceOneAsync(e => e.Id == id, newEntity);

        public Task RemoveAsync(Guid id) => _collection.DeleteOneAsync(e => e.Id == id);
        public Task RemoveRangeAsync(Expression<Func<T, bool>> predicate) => _collection.DeleteManyAsync(predicate);
    }
}
