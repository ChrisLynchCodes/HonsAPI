using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.DTOs
{
    public class AdminEditDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(50)]
        [BsonElement("first-name")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [BsonElement("last-name")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [BsonElement("email")]
        public string? Email { get; set; }


        public string? ImageLink { get; set; }

  
    }
}
