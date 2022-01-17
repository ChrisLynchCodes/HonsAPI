﻿using HonsBackendAPI.Database;
using HonsBackendAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HonsBackendAPI.Services.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMongoCollection<Review> _reviewsCollection;

        public ReviewRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _reviewsCollection = mongoDatabase.GetCollection<Review>(databaseSettings.Value.ReviewsCollectionName);


        }

        public async Task<List<Review>> GetAsync() =>
            await _reviewsCollection.Find(_ => true).ToListAsync();
        public async Task<List<Review>> GetReviewsForProductAsync(string productId) =>
            await _reviewsCollection.Find(x => x.ProductId == productId).ToListAsync();
        public async Task<List<Review>> GetReviewsForCustomerAsync(string customerId) =>
            await _reviewsCollection.Find(x => x.CustomerId == customerId).ToListAsync();

        public async Task<Review?> GetAsync(string id) =>
        await _reviewsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Review newReview) =>
        await _reviewsCollection.InsertOneAsync(newReview);

        public async Task UpdateAsync(string id, Review updatedReview) =>
            await _reviewsCollection.ReplaceOneAsync(x => x.Id == id, updatedReview);

        public async Task RemoveAsync(string id) =>
            await _reviewsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
