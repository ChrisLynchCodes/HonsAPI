using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HonsBackendAPI.Services.Repositories
{
    public class BasketRepository : IBasketRepository
    {


        private readonly IMongoCollection<CustomerBasket> _basketsCollection;

        public BasketRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _basketsCollection = mongoDatabase.GetCollection<CustomerBasket>(databaseSettings.Value.BasketsCollectionName);


        }

        public async Task<List<CustomerBasket>> GetAllAsync() =>
             await _basketsCollection.Find(_ => true).ToListAsync();

        public async Task<CustomerBasket?> GetOneAsync(string basketId) =>
        await _basketsCollection.Find(x => x.Id == basketId).FirstOrDefaultAsync();

        public async Task<CustomerBasket?> GetByCustomerIdAsync(string customerId) =>
       await _basketsCollection.Find(x => x.CustomerId == customerId).FirstOrDefaultAsync();
        public async Task CreateAsync(CustomerBasket newBasket) =>
        await _basketsCollection.InsertOneAsync(newBasket);

        public async Task UpdateAsync(string basketId, CustomerBasket updatedBasket) =>
            await _basketsCollection.ReplaceOneAsync(x => x.Id == basketId, updatedBasket);

        public async Task RemoveAsync(string basketId) =>
            await _basketsCollection.DeleteOneAsync(x => x.Id == basketId);
    }
}
