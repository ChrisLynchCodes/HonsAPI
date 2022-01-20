using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.DTOs
{
    public class CustomerCreateDto
    {


        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

       


    }
}
