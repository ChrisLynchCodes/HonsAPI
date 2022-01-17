namespace HonsBackendAPI.DTOs
{
    public class ProductDto
    {

        public string? Id { get; set; }


        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }


        public string? CategoryId { get; set; }


        public string Description { get; set; } = null!;


        public int StockRemaining { get; set; }


        public string? ImageLink { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime UpdatedAt { get; set; }
    }
}
