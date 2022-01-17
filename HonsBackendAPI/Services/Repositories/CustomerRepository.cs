using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using HonsBackendAPI.ResourceParamaters;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HonsBackendAPI.Services.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customersCollection;

        public CustomerRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _customersCollection = mongoDatabase.GetCollection<Customer>(databaseSettings.Value.CustomersCollectionName);


        }

        public async Task<List<Customer>> GetAsync() =>          
            await _customersCollection.Find(_ => true).ToListAsync();
                      
        public async Task<Customer?> GetAsync(string id) =>
        await _customersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Customer newCustomer) =>
        await _customersCollection.InsertOneAsync(newCustomer);

        public async Task UpdateAsync(string id, Customer updatedCustomer) =>
            await _customersCollection.ReplaceOneAsync(x => x.Id == id, updatedCustomer);

        public async Task RemoveAsync(string id) =>
            await _customersCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<Customer?> CustomerExists(string email) =>
         await _customersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
    }
}
