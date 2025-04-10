using System.ComponentModel.DataAnnotations;
using WebAPI_SoftwareMind.Models.Entities;
/// <summary>
/// Represents a negotiation process for a product.
/// </summary>
public class Negotiation
{
    /// <summary>
    /// The unique identifier of the negotiation.
    /// </summary>
    /// <example>1</example>
    [Key]
    public int NegotiationId { get; set; }
    /// <summary>
    /// The unique identifier of the product involved in the negotiation.
    /// </summary>
    /// <example>12</example>
    [Required]
    public int ProductId { get; set; }
    /// <summary>
    /// The proposed price for the product during the negotiation.
    /// </summary>
    /// <example>150.50</example>
    [Required]
    public decimal ProposedPrice { get; set; }
    /// <summary>
    /// The current status of the negotiation. Possible values: 'Pending', 'Accepted', 'Rejected'.
    /// </summary>
    /// <example>Pending</example>
    [Required]
    public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected
    /// <summary>
    /// The number of attempts made to negotiate the price.
    /// </summary>
    /// <example>2</example>
    public int Attempts { get; set; } = 0;
    /// <summary>
    /// The expiration date of the negotiation, after which it is no longer valid.
    /// </summary>
    /// <example>2025-04-17T00:00:00Z</example>
    public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(7);
    /// <summary>
    /// The product associated with the negotiation.
    /// </summary>
    public Product? Product { get; set; }
}