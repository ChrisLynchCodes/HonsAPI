using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.DTOs
{
    public class AddressCreateDto
    {



        [Required]
        [MaxLength(200)]
        public string FirstLine { get; set; } = null!;


        [MaxLength(200)]
        public string SecondLine { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string MobileNumber { get; set; } = null!;


        [MaxLength(50)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Country { get; set; } = null!;

       
        
    }
}
