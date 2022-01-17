using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.DTOs
{
    public class ReviewCreateDto
    {




        [Required]
        public int Rating { get; set; }

        [Required]
        public string Body { get; set; } = null!;

    }
}
