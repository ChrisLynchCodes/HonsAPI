using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.Models
{
    public class OrderLine
    {
       

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("product-id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId { get; set; }

        [BsonElement("order-id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrderId { get; set; }

        [Required]
        [BsonElement("quantity")]
        public int Quantity { get; set; }
       

        [Required]
        [MaxLength(50)]
        [BsonElement("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(50)]
        [BsonElement("updated-at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
