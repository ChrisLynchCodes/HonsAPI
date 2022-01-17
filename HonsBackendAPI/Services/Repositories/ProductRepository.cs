using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HonsBackendAPI.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        //Get injected dependencies

        public ProductRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);


            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _productsCollection = mongoDatabase.GetCollection<Product>(databaseSettings.Value.ProductsCollectionName);


        }

        public async Task<List<Product>> GetAsync() =>
               await _productsCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetAsync(string id) =>
               await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product newProduct) =>
        await _productsCollection.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string id, Product updatedProduct) =>
            await _productsCollection.ReplaceOneAsync(x => x.Id == id, updatedProduct);

        public async Task RemoveAsync(string id) =>
            await _productsCollection.DeleteOneAsync(x => x.Id == id);

        
    }
}
