namespace HonsBackendAPI.DTOs
{
    public class AddressDto
    {

        public string? Id { get; set; }

       
        public string FirstLine { get; set; } = null!;


   
        public string SecondLine { get; set; } = null!;

      
        public string PostalCode { get; set; } = null!;

    
        public string City { get; set; } = null!;

       
        public string MobileNumber { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;

       
        public string Country { get; set; } = null!;

      
        public string? CustomerId { get; set; } 

     
        public DateTime CreatedAt { get; set; }

       
        public DateTime UpdatedAt { get; set; }
    }
}
