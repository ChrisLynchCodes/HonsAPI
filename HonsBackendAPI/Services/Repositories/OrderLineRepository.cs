using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HonsBackendAPI.Services.Repositories
{
    //Orderlines are only ever exposed through the Orders Endpoint. An orderid must be passed in.
    public class OrderLineRepository : IOrderLineRepository
    {

        private readonly IMongoCollection<OrderLine> _orderLinesCollection;


        public OrderLineRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _orderLinesCollection = mongoDatabase.GetCollection<OrderLine>(databaseSettings.Value.OrderLinesCollectionName);


        }


        //Get all orderlines that contain the passed in orderid
        public async Task<List<OrderLine>> GetAllAsync(string orderId) =>
             await _orderLinesCollection.Find(x => x.OrderId == orderId).ToListAsync();


        //Get a specific orderline in an order
        public async Task<OrderLine?> GetOneAsync(string orderId, string orderLineId)
        {
            var orderLines = await _orderLinesCollection.Find(x => x.OrderId == orderId).ToListAsync();

            return orderLines.Find(x => x.Id == orderLineId);

        }

        





        public async Task CreateAsync(OrderLine newOrderLine) =>
            await _orderLinesCollection.InsertOneAsync(newOrderLine);

        public async Task UpdateAsync(string id, OrderLine updatedOrderLine) =>
            await _orderLinesCollection.ReplaceOneAsync(x => x.Id == id, updatedOrderLine);

        public async Task RemoveAsync(string id) =>
            await _orderLinesCollection.DeleteOneAsync(x => x.Id == id);

        public async Task RemoveManyAsync(string orderId) =>        
        await _orderLinesCollection.DeleteManyAsync(x => x.OrderId == orderId);
        
    }
}
