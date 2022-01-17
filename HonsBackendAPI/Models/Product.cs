using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }

        [Required]
        [BsonElement("name")]
        public string ProductName { get; set; } = null!;

        [Required]
        [BsonElement("price")]
        public decimal Price { get; set; }

        [Required]
        [BsonElement("category-id")]
        public string? CategoryId { get; set; }

        [Required]
        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [Required]
        [BsonElement("stock-remaining")]
        public int StockRemaining { get; set; }

        [Required]
        [BsonElement("image-link")]
        public string? ImageLink { get; set; }

        [Required]
        [BsonElement("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [BsonElement("updated-at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
