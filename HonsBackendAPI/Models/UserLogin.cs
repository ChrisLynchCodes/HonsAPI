using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.Models
{
    public class UserLogin
    {
        //public UserLogins() { }
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

    }
}
