using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(50)]
        [BsonElement("name")]
        public string CategoryName { get; set; } = null!;

        [Required]
        [BsonElement("thumbnail")]
        public string Thumbnail { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        [BsonElement("description")]
        public string Description { get; set; } = null!;


        [Required]
        [BsonElement("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [BsonElement("updated-at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
