using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HonsBackendAPI.Services.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categoriesCollection;

        //Get injected dependencies

        public CategoryRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);


            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _categoriesCollection = mongoDatabase.GetCollection<Category>(databaseSettings.Value.CategoriesCollectionName);


        }

        

        public async Task<List<Category>> GetAllAsync() =>
               await _categoriesCollection.Find(_ => true).ToListAsync();

        public async Task<Category?> GetOneAsync(string categoryId) =>
               await _categoriesCollection.Find(x => x.Id == categoryId).FirstOrDefaultAsync();

        public async Task CreateAsync(Category newCategory) =>
        await _categoriesCollection.InsertOneAsync(newCategory);

        public async Task UpdateAsync(string categoryId, Category updatedCategory) =>
            await _categoriesCollection.ReplaceOneAsync(x => x.Id == categoryId, updatedCategory);

        public async Task RemoveAsync(string categoryId) =>
            await _categoriesCollection.DeleteOneAsync(x => x.Id == categoryId);
    }
}
