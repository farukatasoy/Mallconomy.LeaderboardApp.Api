using Mallconomy.LeaderboardApp.Data.Configurations;
using Mallconomy.LeaderboardApp.Data.Entities;
using Mallconomy.LeaderboardApp.Data.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mallconomy.LeaderboardApp.Data.Services
{
    public class Service<T> : IService<T> where T : BaseEntity, new()
    {
        private readonly IMongoCollection<T> _collection;

        public Service(IOptions<MallconomyDatabaseSettings> mallconomyDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                mallconomyDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mallconomyDatabaseSettings.Value.DatabaseName);

            //burada T'nin tipine gore collection'in secilmesini kurgulamak gerekiyor
            _collection = mongoDatabase.GetCollection<T>(
                mallconomyDatabaseSettings.Value.LeaderboardCollectionName);
        }

        public async Task<List<T>> GetAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<T?> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(T newRecord) =>
            await _collection.InsertOneAsync(newRecord);

        public async Task UpdateAsync(string id, T updatedRecord) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedRecord);

        public async Task RemoveAsync(string id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}