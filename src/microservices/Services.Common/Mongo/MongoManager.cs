using MongoDB.Driver;
using Services.Common.Domain;

namespace Services.Common.Mongo
{
    public class MongoManager : IMongoManager
    {
        private IMongoDatabase _mongoDatabase;
        public void RegisterDatabase(IMongoDatabase database)
        {
            _mongoDatabase = database;
        }

        public IMongoCollection<T> GetCollection<T>() where T : IDomain
        {
            return _mongoDatabase.GetCollection<T>(typeof(T).Name);
        }
    }
}
