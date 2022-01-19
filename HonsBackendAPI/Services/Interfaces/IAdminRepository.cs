using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IAdminRepository
    {
        Task CreateAsync(Admin newAdmin);
        Task<List<Admin>> GetAllAsync();
        Task<Admin?> GetOneAsync(string adminId);
        Task<Admin?> GetByEmailAsync(string email);
        Task RemoveAsync(string adminId);
        Task UpdateAsync(string adminId, Admin updatedAdmin);
    }
}