using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.Models
{
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(50)]
        [BsonElement("first-name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [BsonElement("last-name")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [BsonElement("password-hash")]
        public string PasswordHash { get; set; } = null!;


        [Required]
        [BsonElement("password-salt")]
        public string? PasswordSalt { get; set; } 

  

        [Required]
        [BsonElement("role")]
        public string? Role { get; set; } = "Admin";

        [Required]
        [BsonElement("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [BsonElement("updated-at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
