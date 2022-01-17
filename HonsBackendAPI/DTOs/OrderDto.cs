namespace HonsBackendAPI.DTOs
{
    public class OrderDto
    {
        
        public string? Id { get; set; }

     
        public decimal Total { get; set; }

      
        public string? CustomerId { get; set; }

     
        public DateTime ExpectedDeliveryDate { get; set; }

     
        public bool Completed { get; set; }


        public DateTime CreatedAt { get; set; } 


        public DateTime UpdatedAt { get; set; } 







    }
}
 