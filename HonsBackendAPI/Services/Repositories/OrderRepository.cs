using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HonsBackendAPI.Services.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _ordersCollection;
        public OrderRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _ordersCollection = mongoDatabase.GetCollection<Order>(databaseSettings.Value.OrdersCollectionName);


        }
        public async Task<List<Order>> GetAsync() =>
                await _ordersCollection.Find(_ => true).ToListAsync();
        public async Task<Order?> GetAsync(string id) =>
        await _ordersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<Order>> GetForAsync(string customerId) =>
         await _ordersCollection.Find(x => x.CustomerId == customerId).ToListAsync();
        public async Task CreateAsync(Order newOrder) =>
        await _ordersCollection.InsertOneAsync(newOrder);

        public async Task UpdateAsync(string id, Order updatedOrder) =>
            await _ordersCollection.ReplaceOneAsync(x => x.Id == id, updatedOrder);

        public async Task RemoveAsync(string id) =>
            await _ordersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
