using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.DTOs
{
    public class AdminCreateDto
    {


        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; } 

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; } 

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

       
    }
}
