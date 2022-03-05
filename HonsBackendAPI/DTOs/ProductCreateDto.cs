using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.DTOs
{
    public class ProductCreateDto
    {



        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? CategoryId { get; set; }


        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public int StockRemaining { get; set; }

        [Required]
        public string? ImageLink { get; set; }


    }
}
