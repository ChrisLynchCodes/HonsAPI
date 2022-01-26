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

        public async Task<List<Product>> GetAllAsync() =>
               await _productsCollection.Find(_ => true).ToListAsync();
        public async Task<List<Product>> GetNAsync(int ammount) =>
              await _productsCollection.Find(_ => true).Limit(ammount).ToListAsync();

        public async Task<Product?> GetOneAsync(string productId) =>
               await _productsCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
        public async Task<List<Product>> GetProductsByCategoryAsync(string categoryId)
        {

            // Filter definition (analyzer provides mql as information message)
            //var filter = Builders<Product>.Filter.Eq(m => m.CategoryId, categoryId);


            // Sort definition (analyzer provides mql as information message)
            var sort = Builders<Product>.Sort.Ascending(m => m.ProductName);
        
            return await _productsCollection.Find(x => x.CategoryId == categoryId).Sort(sort).ToListAsync();

            
        }



        public async Task CreateAsync(Product newProduct) =>
        await _productsCollection.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string productId, Product updatedProduct) =>
            await _productsCollection.ReplaceOneAsync(x => x.Id == productId, updatedProduct);

        public async Task RemoveAsync(string productId) =>
            await _productsCollection.DeleteOneAsync(x => x.Id == productId);


    }
}
