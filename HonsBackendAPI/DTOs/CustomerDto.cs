using HonsBackendAPI.JWT;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HonsBackendAPI.DTOs
{
    public class CustomerDto
    {

        //DTO used for returning data so no data annotatins needed to validate incoming data
    
        public string? Id { get; set; }

        public string FirstName { get; set; } = null!;

 
        public string LastName { get; set; } = null!;

 
        public string Email { get; set; } = null!;

       
        public string? ConfirmPassword { get; set; }


        public DateTime CreatedAt { get; set; }

      
        public DateTime UpdatedAt { get; set; }

    }
}
