using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Services.Common.Domain;

namespace Services.Common.Mongo
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton<IMongoManager, MongoManager>();
            return services;
        }


        public static IServiceCollection AddMongoCollection<T>(this IServiceCollection services) where T : IDomain =>
            services.AddTransient<IMongoRepository<T>, MongoRepository<T>>();


        public static IApplicationBuilder UseMongo(this IApplicationBuilder builder, string serviceName)
        { 
            var suffix = serviceName.Split('.')[1];
            var client = new MongoClient(@"mongodb://mongo:27017");
            var database = client.GetDatabase(suffix);
            var manager = builder.ApplicationServices.GetService<IMongoManager>();
            manager.RegisterDatabase(database);
            return builder;
        }
    }
}
