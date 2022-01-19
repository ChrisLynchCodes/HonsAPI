using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category newCategory);
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetOneAsync(string categoryId);
        Task RemoveAsync(string icategoryIdd);
        Task UpdateAsync(string categoryId, Category updatedCategory);
    }
}