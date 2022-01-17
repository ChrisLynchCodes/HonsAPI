using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HonsBackendAPI.Services.Repositories
{
    public class AddressRepository : IAddressRepository
    {

        private readonly IMongoCollection<Address> _addressesCollection;

        public AddressRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _addressesCollection = mongoDatabase.GetCollection<Address>(databaseSettings.Value.AddressesCollectionName);


        }

        //Get all addresses for a specific customer
        public async Task<List<Address>> GetAsync(string customerId) =>
             await _addressesCollection.Find(x => x.CustomerId == customerId).ToListAsync();



        //Get specific address for a specific customer
        public async Task<Address?> GetOneAsync(string addressId) =>
         await _addressesCollection.Find(x => x.Id == addressId).FirstOrDefaultAsync();
        


        public async Task CreateAsync(Address newAddress) =>
        await _addressesCollection.InsertOneAsync(newAddress);

        public async Task UpdateAsync(string addressId, Address updatedAddress) =>
            await _addressesCollection.ReplaceOneAsync(x => x.Id == addressId, updatedAddress);

        public async Task RemoveAsync(string id) =>
            await _addressesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
