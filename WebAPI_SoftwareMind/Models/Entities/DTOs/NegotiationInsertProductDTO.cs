using System.ComponentModel.DataAnnotations;

namespace WebAPI_SoftwareMind.Models.Entities.DTOs
{
    /// <summary>
    /// DTO used for inserting a new negotiation with a product.
    /// This model is used when creating a new negotiation for a product, where the user proposes a price.
    /// </summary>
    public class NegotiationInsertProductDTO
    {
        /// <summary>
        /// The unique identifier for the product being negotiated.
        /// </summary>
        /// <example>1</example>
        [Required]
        public int ProductId { get; set; }
        /// <summary>
        /// The proposed price for the product in the negotiation.
        /// The price must be greater than zero.
        /// </summary>
        /// <example>100.50</example>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be grater than 0")]
        public decimal ProposedPrice { get; set; }
    }
}
