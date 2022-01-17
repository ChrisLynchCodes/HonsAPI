using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [BsonElement("total")]
        public decimal Total { get; set; }
       
        [Required]
        [BsonElement("customer-id")]
        public string? CustomerId { get; set; }

        [Required]
        [BsonElement("expected-delivery-date")]
        public DateTime ExpectedDeliveryDate { get; set; } = DateTime.Now.AddDays(5);

        [Required]
        [BsonElement("completed")]
        public bool Completed { get; set; }


        [Required]
        [BsonElement("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [BsonElement("updated-at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        
    }
}
