namespace HonsBackendAPI.DTOs
{
    public class CategoryDto
    {
      
        public string? Id { get; set; }

        public string CategoryName { get; set; } = null!;


        public string Thumbnail { get; set; } = null!;


        public string Description { get; set; } = null!;


        


        public DateTime CreatedAt { get; set; }


        public DateTime UpdatedAt { get; set; }
    }
}
