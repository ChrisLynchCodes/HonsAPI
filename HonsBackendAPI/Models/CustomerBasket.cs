using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.Models
{
    public class CustomerBasket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

      
        [BsonElement("customer-id")]
        public string? CustomerId { get; set; }

        [BsonElement("address-id")]
        public string? AddressId { get; set; }

        [BsonElement("basket-products")]
        public List<BasketProducts> BasketProducts { get; set; } = new List<BasketProducts>();

  
        [BsonElement("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    
        [BsonElement("updated-at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }

    public class BasketProducts
    {
      
        public int Quantity { get; set; }
        public string? ProductId { get; set; }
        public string? StripePrice { get; set; }

    }
}
