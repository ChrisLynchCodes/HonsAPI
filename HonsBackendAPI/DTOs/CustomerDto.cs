using HonsBackendAPI.JWT;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HonsBackendAPI.DTOs
{
    public class CustomerDto
    {

        //DTO used for returning data so no data annotatins needed to validate incoming data
    
        public string? Id { get; set; }

        public string? FirstName { get; set; }

 
        public string? LastName { get; set; } 

 
        public string? Email { get; set; } 

       
        public string? PasswordHash { get; set; }

        public string? Role { get; set; }

        public string? ImageLink { get; set; }
        public DateTime CreatedAt { get; set; }

      
        public DateTime UpdatedAt { get; set; }

    }
}
