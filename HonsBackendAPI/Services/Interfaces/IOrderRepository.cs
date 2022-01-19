using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order newOrder);
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetOrdersForCustomerAsync(string customerId);
        Task<Order?> GetOneAsync(string orderId);
        Task RemoveAsync(string orderId);
        Task UpdateAsync(string orderId, Order updatedOrder);
    }
}