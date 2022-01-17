using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IReviewRepository
    {
        Task CreateAsync(Review newReview);
        Task<List<Review>> GetAsync();
        Task<List<Review>> GetReviewsForProductAsync(string productId);
        Task<List<Review>> GetReviewsForCustomerAsync(string customerId);
        Task<Review?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Review updatedReview);
    }
}