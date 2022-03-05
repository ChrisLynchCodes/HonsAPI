using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(Product newProduct);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetNAsync(int ammount);
        Task<List<Product>> GetProductsByCategoryAsync(string categoryId);
        Task<List<Product>> GetProductsByNameAsync(string productName);
        Task<Product?> GetOneAsync(string productId);
        Task RemoveAsync(string productId);
        Task UpdateAsync(string productId, Product updatedProduct);
        Task<List<Product>> GetManyAsync(List<string> productIds);
    }
}