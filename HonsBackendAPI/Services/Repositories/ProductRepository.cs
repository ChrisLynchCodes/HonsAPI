using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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



        public async Task<List<Product>> GetAllAsync()
        {
            //var filter = Builders<Product>.Filter.Eq(m => m.CategoryId, categoryId);
            //await _productsCollection.Find(_ => true).ToListAsync();
            var filter = Builders<Product>.Filter.Empty;
            var page = 1;
            var perPage = 20;

            return await _productsCollection.Find(filter).Skip((page - 1) * perPage).Limit(perPage).ToListAsync();
        }

        public async Task<List<Product>> GetNAsync(int ammount) =>
              await _productsCollection.Find(_ => true).Limit(ammount).ToListAsync();

        public async Task<Product?> GetOneAsync(string productId) =>
               await _productsCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();

        public async Task<List<Product>> GetManyAsync(List<string> productIds)
        {

            var filterDef = new FilterDefinitionBuilder<Product>();
            var filter = filterDef.In(x => x.Id, new[] { "61dcaecbf34f7920e33400b0", "620809c5e833d7972d8ed0bb" });
          
        

            return await _productsCollection.Find(filter).ToListAsync();



            //var productObjectIDs = productIds.Select(id => new ObjectId(id));
            //var filter = Builders<Product>.Filter.In(p => p.Id, productObjectIDs);

            //var products = _productsCollection.Find(filter).ToListAsync();

            //return products;
            //_productsCollection.Find(o => o.Any(i => productIds.Contains(o.Id));

        }



        public async Task<List<Product>> GetProductsByCategoryAsync(string categoryId)
        {

            // Filter definition (analyzer provides mql as information message)
            //var filter = Builders<Product>.Filter.Eq(m => m.CategoryId, categoryId);


            // Sort definition (analyzer provides mql as information message)
            var sort = Builders<Product>.Sort.Ascending(m => m.ProductName);

            return await _productsCollection.Find(x => x.CategoryId == categoryId).Sort(sort).ToListAsync();


        }

        public async Task<List<Product>> GetProductsByNameAsync(string productName)
        {
            var filter = Builders<Product>.Filter.Eq(m => m.ProductName, productName);

            return await _productsCollection.Find(filter).ToListAsync();
        }







        public async Task CreateAsync(Product newProduct) =>
        await _productsCollection.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string productId, Product updatedProduct) =>
            await _productsCollection.ReplaceOneAsync(x => x.Id == productId, updatedProduct);

        public async Task RemoveAsync(string productId) =>
            await _productsCollection.DeleteOneAsync(x => x.Id == productId);


    }
}
