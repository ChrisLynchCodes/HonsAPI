using HonsBackendAPI.Models;
using HonsBackendAPI.ResourceParamaters;

namespace HonsBackendAPI.Services.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer newCustomer);
        Task<List<Customer>> GetAsync();
        Task<Customer?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Customer updatedCustomer);
        Task<Customer> CustomerExists(string customerEmail);
    }
}