using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HonsBackendAPI.Services.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IMongoCollection<Admin> _adminsCollection;

        public AdminRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _adminsCollection = mongoDatabase.GetCollection<Admin>(databaseSettings.Value.AdminsCollectionName);


        }

        public async Task<List<Admin>> GetAsync() =>
            await _adminsCollection.Find(_ => true).ToListAsync();

        public async Task<Admin?> GetAsync(string id) =>
        await _adminsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Admin newAdmin) =>
        await _adminsCollection.InsertOneAsync(newAdmin);

        public async Task UpdateAsync(string id, Admin updatedAdmin) =>
            await _adminsCollection.ReplaceOneAsync(x => x.Id == id, updatedAdmin);

        public async Task RemoveAsync(string id) =>
            await _adminsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
