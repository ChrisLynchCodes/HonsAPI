﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.Models
{
    public class Customer
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
        [MaxLength(100)]
        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [BsonElement("password")]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password")]
        [BsonElement("confirm-password")]
        public string? ConfirmPassword { get; set; }



        [Required]
        [BsonElement("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [BsonElement("updated-at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;




    }
}