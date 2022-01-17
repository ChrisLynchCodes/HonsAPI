using System.ComponentModel.DataAnnotations;

namespace HonsBackendAPI.DTOs
{
    public class OrderLineCreateDto
    {


        [Required]
        public string? ProductId { get; set; }


        [Required]
        public int Quantity { get; set; }

     


    }
}
