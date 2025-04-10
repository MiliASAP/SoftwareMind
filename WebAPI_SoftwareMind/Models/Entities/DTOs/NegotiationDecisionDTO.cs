using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI_SoftwareMind.Models.Entities.DTOs
{
    /// <summary>
    /// DTO for negotiating the decision on a product's price.
    /// This model is used when the negotiation process is being decided, either by accepting or rejecting the negotiation.
    /// </summary>
    public class NegotiationDecisionDTO
    {
        /// <summary>
        /// The unique identifier for the negotiation.
        /// </summary>
        /// <example>10</example>
        [Required]
        public int NegotiationId { get; set; }

        /// <summary>
        /// The decision regarding the negotiation, either "accept" or "reject".
        /// </summary>
        /// <example>accept</example>
        [Required]
        [RegularExpression("(?i)^(accept|reject)$", ErrorMessage = "Decision must be either 'accept' or 'reject'.")]
        public string Decision { get; set; } = string.Empty;
    }
}