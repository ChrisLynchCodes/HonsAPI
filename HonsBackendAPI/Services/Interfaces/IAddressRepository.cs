using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IAddressRepository
    {
        Task CreateAsync(Address newAddress);
    
        Task<List<Address>> GetAllAddressesForCustomerAsync(string customerId);
        
        Task<Address?> GetOneAsync(string addressId);
        Task RemoveAsync(string addresId);
        Task RemoveManyAsync(string customerId);
        Task UpdateAsync(string addressId, Address updatedAddress);
    }
}