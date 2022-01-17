using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order newOrder);
        Task<List<Order>> GetAsync();
        Task<List<Order>> GetForAsync(string customerId);
        Task<Order?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Order updatedOrder);
    }
}