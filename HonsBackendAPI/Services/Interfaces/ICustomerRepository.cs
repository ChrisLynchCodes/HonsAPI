using HonsBackendAPI.Models;
using HonsBackendAPI.ResourceParamaters;

namespace HonsBackendAPI.Services.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer newCustomer);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetOneAsync(string customerId);
        Task RemoveAsync(string customerId);
        Task UpdateAsync(string customerId, Customer updatedCustomer);
        Task<Customer?> GetByEmail(string customerEmail);
    }
}