using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Interfaces
{
    public interface IBasketRepository
    {
        Task CreateAsync(CustomerBasket customerBasket);
        Task<List<CustomerBasket>> GetAllAsync();
        Task<CustomerBasket?> GetOneAsync(string basketId);
        Task<CustomerBasket?> GetByCustomerIdAsync(string customerId);
        Task RemoveAsync(string basketId);
        Task UpdateAsync(string basketId, CustomerBasket customerBasket);
    }
}
