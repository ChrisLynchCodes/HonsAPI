using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.DTOs
{
    public class OrderCreateDto
    {


        [Required]
        public string? CustomerId { get; set; }

        [Required]
      
        public string? AddressId { get; set; }




    }
}
