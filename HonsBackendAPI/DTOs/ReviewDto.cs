namespace HonsBackendAPI.DTOs
{
    public class ReviewDto
    {
       
        public string? Id { get; set; }

  
        public string? CustomerId { get; set; }

      
        public string? ProductId { get; set; }

   
        public int Rating { get; set; }

   
        public string Body { get; set; } = null!;

        public DateTime CreatedAt { get; set; }


        public DateTime UpdatedAt { get; set; }
    }

}
