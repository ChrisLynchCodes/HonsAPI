using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IOrderLineRepository
    {
        Task CreateAsync(OrderLine newOrderLine);
      
        Task<OrderLine?> GetOneAsync(string orderId, string orderLineId);
        Task<List<OrderLine>> GetAllAsync(string orderId);
        Task RemoveAsync(string id);
        Task RemoveManyAsync(string id);
        Task UpdateAsync(string id, OrderLine updatedOrderLine);
    }
}