using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Services.Common.Mongo;
using Services.Logs.Domain;

namespace Services.Logs.Repositorys
{
    public class LogRepository : ILogsRepository
    {
        private readonly IMongoRepository<Log> _repository;
        private readonly IMongoManager _manager;

        public LogRepository(IMongoRepository<Log> repository, IMongoManager manager)
        {
            _repository = repository;
            _manager = manager;
        }

        public async Task<IEnumerable<ILog>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task Purge()
        {
            return _manager.GetCollection<Log>().DeleteManyAsync(l => l.Id != Guid.Empty);
        }
    }
}
