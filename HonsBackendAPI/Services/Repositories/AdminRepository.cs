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

        public async Task<List<Admin>> GetAllAsync() =>
            await _adminsCollection.Find(_ => true).ToListAsync();

        public async Task<Admin?> GetOneAsync(string adminId) =>
        await _adminsCollection.Find(x => x.Id == adminId).FirstOrDefaultAsync();
        public async Task<Admin?> GetByEmailAsync(string email) =>
       await _adminsCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task CreateAsync(Admin newAdmin) =>
        await _adminsCollection.InsertOneAsync(newAdmin);

        public async Task UpdateAsync(string adminId, Admin updatedAdmin) =>
            await _adminsCollection.ReplaceOneAsync(x => x.Id == adminId, updatedAdmin);

        public async Task RemoveAsync(string adminId) =>
            await _adminsCollection.DeleteOneAsync(x => x.Id == adminId);
    }
}
