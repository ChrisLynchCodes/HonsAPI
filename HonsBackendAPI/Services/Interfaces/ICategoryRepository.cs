using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category newCategory);
        Task<List<Category>> GetAsync();
        Task<Category?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Category updatedCategory);
    }
}