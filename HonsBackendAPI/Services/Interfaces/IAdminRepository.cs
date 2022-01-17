using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IAdminRepository
    {
        Task CreateAsync(Admin newAdmin);
        Task<List<Admin>> GetAsync();
        Task<Admin?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Admin updatedAdmin);
    }
}