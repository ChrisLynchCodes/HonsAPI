using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IReviewRepository
    {
        Task CreateAsync(Review newReview);
        Task<List<Review>> GetAllAsync();
        Task<List<Review>> GetReviewsForProductAsync(string productId);
        Task<List<Review>> GetReviewsByCustomerAsync(string customerId);
        Task<Review?> GetOneAsync(string reviewId);
        Task RemoveAsync(string reviewId);
        Task RemoveManyAsync(string customerId);
        Task UpdateAsync(string reviewId, Review updatedReview);
    }
}