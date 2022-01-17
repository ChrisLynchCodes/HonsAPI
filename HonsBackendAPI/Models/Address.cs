using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.Models
{
    public class Address
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(200)]
        [BsonElement("first-line")]
        public string FirstLine { get; set; } = null!;

       
        [MaxLength(200)]
        [BsonElement("second-line")]
        public string SecondLine { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        [BsonElement("postalcode")]
        public string PostalCode { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [BsonElement("city")]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [BsonElement("mobile-number")]
        public string MobileNumber { get; set; } = null!;

        
        [MaxLength(50)]
        [BsonElement("phone-number")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [BsonElement("country")]
        public string Country { get; set; } = null!;

        [Required]
        [BsonElement("customer-id")]
        public string? CustomerId { get; set; } 

        [Required]
        [BsonElement("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [BsonElement("updated-at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
