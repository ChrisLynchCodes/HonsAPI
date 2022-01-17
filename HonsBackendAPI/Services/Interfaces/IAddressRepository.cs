using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IAddressRepository
    {
        Task CreateAsync(Address newAddress);
        Task<List<Address>> GetAsync(string customerId);
        Task<Address?> GetOneAsync(string addressId);
        Task RemoveAsync(string id);
        Task UpdateAsync(string addressId, Address updatedAddress);
    }
}