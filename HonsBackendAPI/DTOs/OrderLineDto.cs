namespace HonsBackendAPI.DTOs
{
    public class OrderLineDto
    {
      
        public string? Id { get; set; }

     
        public string? ProductId { get; set; }

    
        public string? OrderId { get; set; }

      
        public int Quantity { get; set; }

        public decimal ProductPrice { get; set; }
       
       
        public DateTime CreatedAt { get; set; } 

   
        public DateTime UpdatedAt { get; set; } 
    }
}
