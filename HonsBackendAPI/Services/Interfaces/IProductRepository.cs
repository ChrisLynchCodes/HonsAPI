using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(Product newProduct);
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetOneAsync(string productId);
        Task RemoveAsync(string productId);
        Task UpdateAsync(string productId, Product updatedProduct);
    }
}