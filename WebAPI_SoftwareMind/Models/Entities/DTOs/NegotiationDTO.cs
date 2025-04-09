using System.ComponentModel.DataAnnotations;

namespace WebAPI_SoftwareMind.Models.Entities.DTOs
{
    public class NegotiationDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be grater than 0")]
        public decimal ProposedPrice { get; set; }
    }
}
