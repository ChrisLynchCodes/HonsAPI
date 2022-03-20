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
        public async Task<List<Order>> GetAllAsync() =>
                await _ordersCollection.Find(_ => true).ToListAsync();
        public async Task<Order?> GetOneAsync(string orderId) =>
        await _ordersCollection.Find(x => x.Id == orderId).FirstOrDefaultAsync();
       
       
        public async Task<List<Order>> GetOrdersForCustomerAsync(string customerId) =>
         await _ordersCollection.Find(x => x.CustomerId == customerId).ToListAsync();

        public async Task<Order> GetOrderForCustomerAsync(string customerId)
        {
            var sort = Builders<Order>.Sort.Descending(m => m.CreatedAt);
            var order = await _ordersCollection.Find(x => x.CustomerId == customerId).Sort(sort).FirstOrDefaultAsync();

            return order;
        }
    
        public async Task CreateAsync(Order newOrder) =>
        await _ordersCollection.InsertOneAsync(newOrder);

        public async Task UpdateAsync(string orderId, Order updatedOrder) =>
            await _ordersCollection.ReplaceOneAsync(x => x.Id == orderId, updatedOrder);

        public async Task RemoveAsync(string orderId) =>
            await _ordersCollection.DeleteOneAsync(x => x.Id == orderId);

        public async Task RemoveManyAsync(string customerId) =>
       await _ordersCollection.DeleteManyAsync(x => x.CustomerId == customerId);
    }
}
