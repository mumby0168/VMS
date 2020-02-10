using MongoDB.Driver;
using Services.Common.Domain;

namespace Services.Common.Mongo
{
    public interface IMongoManager
    {
        void RegisterDatabase(IMongoDatabase database);

        IMongoCollection<T> GetCollection<T>() where T : IDomain;
    }
}
