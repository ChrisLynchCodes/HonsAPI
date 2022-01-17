using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(Product newProduct);
        Task<List<Product>> GetAsync();
        Task<Product?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Product updatedProduct);
    }
}