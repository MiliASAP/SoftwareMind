using System.ComponentModel.DataAnnotations;

namespace WebAPI_SoftwareMind.Models.Entities.DTOs
{
    /// <summary>
    /// DTO used for negotiating an existing product price within an active negotiation.
    /// This model is used when retrying negotiations with a new proposed price.
    /// </summary>
    public class NegotiationNegotiateDTO
    {
        /// <summary>
        /// The unique identifier of the negotiation.
        /// </summary>
        /// <example>1</example>
        [Required]
        public int NegotiationId { get; set; }
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
